using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MonitorEngine
{
    private AppConfig _config;
    private KeyboardHook _hook;
    private StringBuilder _wordBuffer = new StringBuilder();
    private HashSet<string> _alreadyLoggedProcesses = new HashSet<string>();

    public MonitorEngine(AppConfig config)
    {
        _config = config;
    }

    public void Start()
    {
        if (_config.EnableStatistics || _config.EnableModeration)
        {
            _hook = new KeyboardHook();
            _hook.KeyPressed += OnKeyPressed;
        }

        Task.Run(async () =>
        {
            while (true)
            {
                CheckProcesses();
                await Task.Delay(2000);
            }
        });
    }

    public void Stop()
    {
        _hook?.Dispose();
    }

    private void OnKeyPressed(string key)
    {
        if (_config.EnableStatistics)
        {
            try
            {
                File.AppendAllText(_config.KeylogFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] - {key}{Environment.NewLine}");
            }
            catch { }
        }

        if (_config.EnableModeration)
        {
            if (key.Length == 1)
            {
                _wordBuffer.Append(key.ToLower());
            }
            else if (key == "Space" || key == "Return" || key == "Tab")
            {
                string currentWord = _wordBuffer.ToString();
                _wordBuffer.Clear();

                foreach (var forbiddenWord in _config.ForbiddenWords)
                {
                    if (currentWord.Contains(forbiddenWord.ToLower()))
                    {
                        File.AppendAllText(_config.ModerationFilePath, 
                            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ПОПЕРЕДЖЕННЯ: Введено заборонене слово '{forbiddenWord}'{Environment.NewLine}");
                    }
                }
            }
            else if (key == "Back" && _wordBuffer.Length > 0)
            {
                _wordBuffer.Length--;
            }
        }
    }

    private void CheckProcesses()
    {
        var runningProcesses = Process.GetProcesses();

        foreach (var proc in runningProcesses)
        {
            string procName = proc.ProcessName.ToLower();

            if (_config.EnableModeration)
            {
                if (_config.ForbiddenApps.Any(app => procName.Contains(app.ToLower())))
                {
                    try
                    {
                        proc.Kill();
                        File.AppendAllText(_config.ModerationFilePath, 
                            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ЗАБЛОКОВАНО: Спроба запуску '{procName}'{Environment.NewLine}");
                        continue;
                    }
                    catch { }
                }
            }

            if (_config.EnableStatistics)
            {
                if (!_alreadyLoggedProcesses.Contains(procName))
                {
                    _alreadyLoggedProcesses.Add(procName);
                    try
                    {
                        File.AppendAllText(_config.AppLogFilePath, 
                            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ЗАПУЩЕНО: {proc.ProcessName} (ID: {proc.Id}){Environment.NewLine}");
                    }
                    catch { }
                }
            }
        }

        var runningNames = runningProcesses.Select(p => p.ProcessName.ToLower()).ToHashSet();
        _alreadyLoggedProcesses.RemoveWhere(name => !runningNames.Contains(name));
    }
}