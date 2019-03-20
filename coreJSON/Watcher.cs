using System;
using System.IO;
namespace coreJSON
{
	public class Watcher
	{
	    public FileSystemWatcher watcher;
		public delegate void WatcherHandler(FileSystemEventArgs args);
		public event WatcherHandler handler;

		public bool debug = false;
		public bool verbose = false;
	    public Watcher(string path, string fileExt)
	    {
	        watcher = new FileSystemWatcher();
	        watcher.EnableRaisingEvents = true;
	        watcher.Path = path;
	        watcher.Filter = "*.json";
	        watcher.NotifyFilter = 
			    NotifyFilters.CreationTime |
			    NotifyFilters.FileName |
			    NotifyFilters.LastWrite |
			    NotifyFilters.Size;
	        watcher.Changed += new FileSystemEventHandler(FileEventsHandler);
			watcher.Created += new FileSystemEventHandler(FileEventsHandler);
			watcher.Renamed += new RenamedEventHandler(FileEventsHandler);	
	    }

	    private void FileEventsHandler(object sender, FileSystemEventArgs e)
	    {
			try{
				if (verbose)
					Console.WriteLine($"FileEvent { e.ChangeType }");
				if (!FileIsReady(e.FullPath)) return;
				
				handler(e);
			}
			catch(Exception ex){
				if (debug)
					Console.WriteLine($"FileEvent ERROR: { ex.Message } { ex.Source } { ex.StackTrace } ");
			}
	    }

	    private bool FileIsReady(string path)
	    { 
	        //One exception per file rather than several like in the polling pattern
	        try
	        {
	             //If we can't open the file, it's still copying
	             using (var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
	             {
	                 return true;
	             }
	        }
	        catch (IOException ex)
	        {
				if (debug)
					Console.WriteLine("IOException: " + ex.Message);
	            return false;
	        }
	    }
	}
}