using System;
using System.Threading.Tasks;

namespace TaskResolve
{
    internal static class Program
    {
        private static async void GetNumber(int number, int delay, Action<int> numberCallback)
        {
            await Task.Delay(delay);
            numberCallback?.Invoke(number);
        }

        private static void PrintNumber(int number, int delay)
        {
            GetNumber(number, delay, n =>
            {
                Console.WriteLine(n);
            });
        }

        private static Task<int> GetNumberAsync(int number, int delay)
        {
            var taskCompletionSource = new TaskCompletionSource<int>();
            var task = taskCompletionSource.Task;
            GetNumber(number, delay, n =>
            {
                taskCompletionSource.SetResult(n);
            });
            return task;
        }

        private static async Task PrintNumberAsync(int number, int delay)
        {
            var n = await GetNumberAsync(number, delay);
            Console.WriteLine(n);
        }

        private static async Task Main(string[] args)
        {
            PrintNumber(1, 200); //can't await
            Console.WriteLine("---1");
            await PrintNumberAsync(2, 100); //can await
            Console.WriteLine("---2");
        }
    }
}