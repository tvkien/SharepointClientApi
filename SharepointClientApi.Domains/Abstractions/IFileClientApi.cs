using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharepointClientApi.Domains.Abstractions
{
    public interface IFileClientApi
    {
        Task UploadFileAsync(UploadFileRequest request);
    }
}
