using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace eGym.Common.Image
{
    public interface IImageHelper
    {
        Task<string> GetBase64Async(string filePath);
        Task<string> GetBase64Async(System.Drawing.Image source);
        Task<string> GetBase64Async(byte[] source);
        Task<string> GetBase64Async(MemoryStream source);

        Task<System.Drawing.Bitmap> ResizeAsync(System.Drawing.Image source, int? maxWidth = null, int? maxHeight = null);
        Task<System.Drawing.Bitmap> ResizeToSquareAsync(System.Drawing.Image source, int? size = null);
    }
}
