using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Azure.Storage.Service
{
    public interface IStorage
    {
        /// <summary>
        /// Save File
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task Save(Stream fileStream, string name);

        /// <summary>
        /// Query File Of FileName 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetNames();

        /// <summary>
        /// Load file
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Stream> Load(string name);
    }
}
