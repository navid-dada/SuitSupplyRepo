using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using EasyNetQ;

namespace WebApplication.Helper
{
    public class TaskManager
    {
        private ConcurrentDictionary<string, TaskCompletionSource<bool>> _taskCompletionSources;

        public TaskManager()
        {
            _taskCompletionSources = new ConcurrentDictionary<string, TaskCompletionSource<bool>>();
        }

        public TaskCompletionSource<bool> AddWaitingTask(string id)
        {
            var tcs = new TaskCompletionSource<bool>();
            try
            {
                
                _taskCompletionSources.Add(id,tcs);
                return tcs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                tcs.SetCanceled();
                return tcs;
            }
        }

        public void CompleteTask(string id, bool result)
        {
            if (!_taskCompletionSources.ContainsKey(id))
            {
                Console.WriteLine($"there is no such task by id:{id}");
            }

            var task = _taskCompletionSources[id];
            task.SetResult(result);
        }
    }
}