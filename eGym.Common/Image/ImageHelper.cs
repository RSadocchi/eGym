using System;
using System.IO;
using System.Threading.Tasks;
using Draw = System.Drawing;
using Draw2D = System.Drawing.Drawing2D;

namespace eGym.Common.Image
{
    public class ImageHelper : IImageHelper
    {
        public async Task<string> GetBase64Async(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            using (var img = Draw.Image.FromFile(filePath))
                return await GetBase64Async(img);
        }

        public async Task<string> GetBase64Async(Draw.Image source)
        {
            using (var stream = new MemoryStream())
            {
                source.Save(stream, source.RawFormat);
                return await GetBase64Async(stream);
            }
        }

        public async Task<string> GetBase64Async(MemoryStream source) => await GetBase64Async(source.ToArray());

        public async Task<string> GetBase64Async(byte[] source) => await Task.FromResult(Convert.ToBase64String(source));

        public async Task<Draw.Bitmap> ResizeAsync(Draw.Image source, int? maxWidth = null, int? maxHeight = null)
        {
            if (maxWidth.HasValue && !maxHeight.HasValue) maxHeight = maxWidth;
            else if (!maxWidth.HasValue && maxHeight.HasValue) maxWidth = maxHeight;
            else if (!maxWidth.HasValue && !maxHeight.HasValue)
            {
                maxWidth = source.Width / 2;
                maxHeight = source.Height / 2;
            }

            int w, h;
            if (source.Width > source.Height)
            {
                w = maxWidth.Value;
                h = (int)(source.Height * maxHeight.Value / (double)source.Width);
            }
            else
            {
                w = (int)(source.Width * maxWidth.Value / (double)source.Height);
                h = maxHeight.Value;
            }

            var canvas = new Draw.Bitmap(w, h);
            using (var graphics = Draw.Graphics.FromImage(canvas))
            {
                graphics.CompositingQuality = Draw2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = Draw2D.InterpolationMode.HighQualityBicubic;
                graphics.CompositingMode = Draw2D.CompositingMode.SourceCopy;
                graphics.SmoothingMode = Draw2D.SmoothingMode.HighQuality;
                graphics.DrawImage(source, 0, 0, w, h);
            }
            return await Task.FromResult(canvas);
        }

        public async Task<Draw.Bitmap> ResizeToSquareAsync(Draw.Image source, int? size = null)
        {
            if (!size.HasValue)
            {
                if (source.Width > source.Height) size = source.Width;
                else size = source.Height;
            }

            int x = 0, y = 0;
            if (source.Width > source.Height)
                y = (source.Width - source.Height) / 2;
            else if (source.Width < source.Height)
                x = (source.Height - source.Width) / 2;
            var canvas = new Draw.Bitmap(size.Value, size.Value);
            using (var graphics = Draw.Graphics.FromImage(canvas))
            {
                graphics.CompositingQuality = Draw2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = Draw2D.InterpolationMode.HighQualityBicubic;
                graphics.CompositingMode = Draw2D.CompositingMode.SourceCopy;
                graphics.SmoothingMode = Draw2D.SmoothingMode.HighQuality;
                graphics.DrawImage(source, x, y, size.Value - x - x, size.Value - y - y);
            }
            return await Task.FromResult(canvas);
        }
    }
}
