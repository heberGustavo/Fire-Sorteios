using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sorteio.Models;
using Sorteio.Portal.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sorteio.Controllers
{
    public class ArquivosController : Controller
    {
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<ActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file != null)
                {
                    var uniqueFileName = file.FileName;
                    byte[] fileBytes;
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    var extensao = Path.GetExtension(file.FileName);

                    var caminhoArquivo = "";

                    if (extensao.Equals(".pdf") || extensao.Equals(".docx") || extensao.Equals(".doc"))
                        caminhoArquivo = UploadHelper.UploadFile(fileBytes, extensao);
                    else if (extensao.Equals(".png") || extensao.Equals(".jpeg") || extensao.Equals(".jpg"))
                        caminhoArquivo = UploadHelper.UploadFile(fileBytes, extensao);

                    return Json(new { erro = false, caminhoArquivo });
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //[HttpPost]
        //[Route("[controller]/[action]")]
        //public async Task<ActionResult> Download([FromBody] ArquivoDownloadBody body)
        //{
        //    try
        //    {
        //        var caminho_arquivo = EncryptProvider.AESDecrypt(body.link, DataDictionary.KEY, DataDictionary.IV);

        //        return Json(new { erro = false, caminho_arquivo = caminho_arquivo });

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
    }
}
