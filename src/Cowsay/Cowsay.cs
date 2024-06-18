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

            // Starting the process
            process.Start();

            // Write the message to the standard input of the cowsay process
            using (StreamWriter writer = process.StandardInput)
            {
                if (writer.BaseStream.CanWrite)
                {
                    writer.WriteLine(message); 
                }
            }

            // Handles outputs and error
            string stdout = process.StandardOutput.ReadToEnd();
            string stderr = process.StandardError.ReadToEnd();

            // Handle reply from stdout and stderr
            OnReply(stdout);
            OnReply(stderr);

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
