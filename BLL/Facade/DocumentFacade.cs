using DAL.Repository;
using DL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public class DocumentFacade
    {
        private IUnitOfWork _uow;

        public DocumentFacade()
        {
            _uow = new UnitOfWork();
        }

        public DocumentFacade(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Add(Document doc)
        {
            _uow.DocumentRepository.Insert(doc);
            _uow.Save();
        }

        public bool isValidExtension(string extension)
        {
            ArrayList alFileTypes =
                new ArrayList { ".ppt", ".pptx", ".mp4" };

            if (!alFileTypes.Contains(extension))
            {
                return false;
            }
            return true;
        }

        public Document.DocumentType getDocumentType(string extension)
        {
            if(extension == ".ppt" || extension == ".pptx")
            {
                return Document.DocumentType.File;
            }

            return Document.DocumentType.Video;
        }
    }
}
