using Microsoft.AspNetCore;

namespace ESTA.Helpers
{
    public class LogManager
    {
        private readonly IHostEnvironment hostEnvironment;

        public LogManager(IHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        
        }

        public  void WriteInLogFile( string str) 
        {
            try
            { 
             
        File.AppendAllText(hostEnvironment.ContentRootPath+"/Esta_log/Log.txt",DateTime.Now.ToLongTimeString()+" -> "+str+ " \n \n");
        
            }
            catch (Exception ex)
            {

                throw;
            }
    
        
        }



    }
}
