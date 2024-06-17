using System;
using System.Diagnostics;

namespace Class
{
    public class Cowsay
    {
        public event EventHandler<string> Reply;

        public void Say(string message)
        {
            // Start the cowsay process
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cowsay", // Set the command to run cowsay
                    Arguments = message, // Pass the message as an argument
                    RedirectStandardOutput = true, // Redirect standard output to capture cowsay's output
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false, // Required to redirect input/output
                    CreateNoWindow = true // Do not create a console window
                }
            };

            // Handle outputz and errorz
            process.OutputDataReceived += (sender, args) => OnReply(args.Data); // Handle standard output
            process.ErrorDataReceived += (sender, args) => OnReply(args.Data); // Handle error output

            // Start
            process.Start();
            process.BeginOutputReadLine(); // Begin reading output asynchronously
            process.BeginErrorReadLine(); // Begin reading error output asynchronously

            // Wait for the process to finish
            process.WaitForExit();
        }

        protected virtual void OnReply(string reply)
        {
            if (!string.IsNullOrEmpty(reply))
            {
                Reply?.Invoke(this, reply); // Invoke the Reply event with the process output
            }
        }
    }
}
