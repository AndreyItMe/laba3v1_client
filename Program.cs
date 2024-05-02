using System;
using System.Net;

class TextFileClient
{
    const string SERVER_URL = "http://localhost:8080/";
    //const string SERVER_URL = "192.168.1.54";
    //const string SERVER_URL = "http://192.168.1.54:8080/";

    public void GetFile(string fileName)
    {
        //string url = SERVER_URL + fileName;
        string url = SERVER_URL + "get/" + fileName;
        WebClient client = new WebClient();
        try
        {
            string content = client.DownloadString(url);
            Console.WriteLine($"Content of file '{fileName}':\n{content}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error downloading file '{fileName}': {ex.Message}");
        }
    }

    public void PutFile(string fileName, string content)
    {
        //string url = SERVER_URL + fileName; // убрать "put/"
        string url = SERVER_URL + "put/" + fileName;
        WebClient client = new WebClient();
        try
        {
            //client.UploadString(url, "PUT", content);
            client.UploadString(url, content);
            Console.WriteLine($"File '{fileName}' uploaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error uploading file '{fileName}': {ex.Message}");
        }
    }

    public void PostFile(string fileName, string content)
    {
        string url = SERVER_URL + "post/" + fileName;
        WebClient client = new WebClient();
        try
        {
            client.UploadString(url, "POST", content);
            Console.WriteLine($"Content of file '{fileName}' appended successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error appending content to file '{fileName}': {ex.Message}");
        }
    }

    public void DeleteFile(string fileName)
    {
        string url = SERVER_URL + "delete/" + fileName;
        WebClient client = new WebClient();
        try
        {
            client.UploadData(url, "DELETE", new byte[0]);
            Console.WriteLine($"File '{fileName}' deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting file '{fileName}': {ex.Message}");
        }
    }

    public void CopyFile(string sourceFileName, string destinationDirectory = "...")
    {
        //в условии написано что служба должна реализовывать метод, а не GET + POST так как это реализация на уровне клиента

        string url = SERVER_URL + "copy/" + sourceFileName + "/" + destinationDirectory;
        WebClient client = new WebClient();
        try
        {
            client.UploadData(url, "PUT", new byte[0]);

            //url = "http://localhost:8080/copy/e";
            //client.UploadData(url, new byte[0]);
            Console.WriteLine($"File '{sourceFileName}' copied to '{destinationDirectory}' successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error copying file '{sourceFileName}' to '{destinationDirectory}': {ex.Message}");
        }
    }

    public void MoveFile(string sourceFileName, string destinationDirectory = "...")
    {
        string url = SERVER_URL + "move/" + sourceFileName + "/" + destinationDirectory;
        WebClient client = new WebClient();
        try
        {
            //client.UploadData(url, "PUT", new byte[0]);
            client.UploadData(url, new byte[0]);
            Console.WriteLine($"File '{sourceFileName}' moved to '{destinationDirectory}' successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error moving file '{sourceFileName}' to '{destinationDirectory}': {ex.Message}");
        }
    }
/*
    public byte[] UploadData(Uri address, string? method, byte[] data)
    {
        ArgumentNullException.ThrowIfNull(address);
        ArgumentNullException.ThrowIfNull(data);

        method ??= MapToDefaultMethod(address);

        StartOperation();
        try
        {
            WebRequest request;
            return UploadDataInternal(address, method, data, out request);
        }
        finally
        {
            EndOperation();
        }
    }
*/
    public void MakeDirectory(string directoryName)
    {
        string url = SERVER_URL + "mkdir/" + directoryName;
        WebClient client = new WebClient();
        try
        {
            client.UploadData(url, "POST", new byte[0]);
            Console.WriteLine($"Directory '{directoryName}' created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating directory '{directoryName}': {ex.Message}");
        }
    }

    public void ChangeDirectory(string directory)
    {
        string url = SERVER_URL + "chdir/" + directory;
        WebClient client = new WebClient();
        try
        {
            client.UploadData(url, "POST", new byte[0]);
            Console.WriteLine($"Changed directory to '{directory}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error changing directory to '{directory}': {ex.Message}");
        }
    }
    public void DeleteDirectory(string directoryName)
    {
        string url = SERVER_URL + "deletedir/" + directoryName;
        WebClient client = new WebClient();
        try
        {
            client.UploadData(url, "DELETE", new byte[0]);
            Console.WriteLine($"Directory '{directoryName}' and its contents deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting directory '{directoryName}': {ex.Message}");
        }
    }
    public void GoToParentDirectory()
    {
        string url = SERVER_URL + "parent";
        WebClient client = new WebClient();
        try
        {
            string response = client.DownloadString(url);
            Console.WriteLine(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error changing to parent directory: {ex.Message}");
        }
    }

    public void ListFilesRecursive()
    {
        string url = SERVER_URL + "list";
        WebClient client = new WebClient();
        try
        {
            string response = client.DownloadString(url);
            Console.WriteLine(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error listing files: {ex.Message}");
        }
    }
    public void RenameFile(string currentFileName, string newFileName)
    {
        string url = SERVER_URL + "renamefile/" + currentFileName + "/" + newFileName;
        WebClient client = new WebClient();
        try
        {
            client.UploadData(url, "PUT", new byte[0]);
            Console.WriteLine($"File '{currentFileName}' renamed to '{newFileName}' successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error renaming file '{currentFileName}' to '{newFileName}': {ex.Message}");
        }
    }

    public void RenameDirectory(string currentDirectoryName, string newDirectoryName)
    {
        string url = SERVER_URL + "renamedir/" + currentDirectoryName + "|/" + newDirectoryName;
        WebClient client = new WebClient();
        try
        {
            client.UploadData(url, "PUT", new byte[0]);
            Console.WriteLine($"Directory '{currentDirectoryName}' renamed to '{newDirectoryName}' successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error renaming directory '{currentDirectoryName}' to '{newDirectoryName}': {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        TextFileClient client = new TextFileClient();
        string command;
        string action;

        do
        {
            Console.WriteLine("\nAvailable commands:");
            Console.WriteLine("1. Get file (usage: get [filename])");
            Console.WriteLine("2. Put file (usage: put [filename] [content])");
            Console.WriteLine("3. Post file (usage: post [filename] [content])");
            Console.WriteLine("4. Delete file (usage: delete [filename])");
            Console.WriteLine("5. Copy file (usage: copy [sourceFileName] [destinationFileName])");
            Console.WriteLine("6. Move file (usage: move [sourceFileName] [destinationFileName])");
/*
            Console.WriteLine("7. Make directory (usage: mkdir [directoryName])");
            Console.WriteLine("8. Change directory (usage: chdir [directoryName])");
            Console.WriteLine("9. Go to parent directory (usage: parent)");
            Console.WriteLine("10. List files (usage: list)");
            Console.WriteLine("11. Delete directory (usage: deletedir [directoryName])");
            Console.WriteLine("12. Rename file (usage: renamefile [currentFileName] [newFileName])");
            Console.WriteLine("13. Rename directory (usage: renamedir [currentDirectoryName] [newDirectoryName])");
            Console.WriteLine("14. Exit");
*/
            Console.Write("\nEnter command: ");
            command = Console.ReadLine().Trim();

            string[] commandParts = command.Split(' ');
            action = commandParts[0].ToLower();

            switch (action)
            {
                case "get":
                    if (commandParts.Length < 2)
                    {
                        Console.WriteLine("Usage: get [filename]");
                        break;
                    }
                    string getFile = commandParts[1];
                    client.GetFile(getFile);
                    break;

                case "put":
                    if (commandParts.Length < 3)
                    {
                        Console.WriteLine("Usage: put [filename] [content]");
                        break;
                    }
                    string putFile = commandParts[1];
                    string putContent = string.Join(" ", commandParts, 2, commandParts.Length - 2);
                    client.PutFile(putFile, putContent);
                    break;

                case "post":
                    if (commandParts.Length < 3)
                    {
                        Console.WriteLine("Usage: post [filename] [content]");
                        break;
                    }
                    string postFile = commandParts[1];
                    string postContent = string.Join(" ", commandParts, 2, commandParts.Length - 2);
                    client.PostFile(postFile, postContent);
                    break;

                case "delete":
                    if (commandParts.Length < 2)
                    {
                        Console.WriteLine("Usage: delete [filename]");
                        break;
                    }
                    string deleteFile = commandParts[1];
                    client.DeleteFile(deleteFile);
                    break;

                case "copy":
                    if (commandParts.Length < 3)
                    {
                        Console.WriteLine("Usage: copy [sourceFileName] [destinationFileName]");
                        break;
                    }
                    string sourceFile = commandParts[1];
                    string destinationFile = commandParts[2];
                    client.CopyFile(sourceFile, destinationFile);
                    break;

                case "move":
                    if (commandParts.Length < 3)
                    {
                        Console.WriteLine("Usage: move [sourceFileName] [destinationFileName]");
                        break;
                    }
                    string moveSourceFile = commandParts[1];
                    string moveDestinationFile = commandParts[2];
                    client.MoveFile(moveSourceFile, moveDestinationFile);
                    break;

                case "mkdir":
                    if (commandParts.Length < 2)
                    {
                        Console.WriteLine("Usage: mkdir [directoryName]");
                        break;
                    }
                    string directoryName = commandParts[1];
                    client.MakeDirectory(directoryName);
                    break;

                case "chdir":
                    if (commandParts.Length < 2)
                    {
                        Console.WriteLine("Usage: chdir [directoryName]");
                        break;
                    }
                    string targetDirectory = commandParts[1];
                    client.ChangeDirectory(targetDirectory);
                    break;

                case "parent":
                    client.GoToParentDirectory();
                    break;

                case "list":
                    client.ListFilesRecursive();
                    break;
                case "deletedir":
                    if (commandParts.Length < 2)
                    {
                        Console.WriteLine("Usage: deletedir [directoryName]");
                        break;
                    }
                    string directoryToDelete = commandParts[1];
                    client.DeleteDirectory(directoryToDelete);
                    break;
                case "renamefile":
                    if (commandParts.Length > 2)
                        client.RenameFile(commandParts[1], commandParts[2]);
                    else
                        Console.WriteLine("Invalid arguments. Usage: renamefile [currentFileName] [newFileName]");
                    break;
                case "renamedir":
                    if (commandParts.Length > 2)
                        client.RenameDirectory(commandParts[1], commandParts[2]);
                    else
                        Console.WriteLine("Invalid arguments. Usage: renamedir [currentDirectoryName] [newDirectoryName]");
                    break;
                case "exit":
                    Console.WriteLine("Exiting...");
                    break;

                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }

        } while (!action.Equals("exit"));
    }
}
