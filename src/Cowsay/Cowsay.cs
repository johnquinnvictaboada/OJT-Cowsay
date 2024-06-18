using System;
using System.Diagnostics;

namespace Class
{
    public class Cowsay
    {
        public event EventHandler<string>? Reply;

        public void Say(string message)
        {
            // Start the cowsay process
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cowsay", // Set the command to run cowsay
                    RedirectStandardOutput = true, // Redirect standard output to capture cowsay's output
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false, // Required to redirect input/output
                    // CreateNoWindow = true // Do not create a console window
                }
            };

            // Start
            process.Start();

            // Handle output and errors
            // process.OutputDataReceived += (sender, args) => OnReply(args.Data); // Handle standard output
            // process.ErrorDataReceived += (sender, args) => OnReply(args.Data); // Handle error output

            // process.BeginOutputReadLine(); // Begin reading output asynchronously
            // process.BeginErrorReadLine(); // Begin reading error output asynchronously

            //Write the message to the standard input of the cowsay process

            //The "using" statement ensures that the disposable instance is disposed
            // using (StreamWriter writer = process.StandardInput) {
            //     if (writer.BaseStream.CanWrite){ //Checks if the stream of StreamWriter supports writing
            //         writer.WriteLine(message); 
            //     }
            // }

            //If using statement is not used we have to close the StreamWriter properly
            StreamWriter writer = process.StandardInput;
            writer.WriteLine(message);
            writer.Close(); //Closes input stream properly

            //Handles outputs and error
            string stdout = process.StandardOutput.ReadToEnd();
            string stderr = process.StandardError.ReadToEnd();

            // Console.WriteLine(stdout,stderr);
            //Handle reply from stdout and stderr
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
