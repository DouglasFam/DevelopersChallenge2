using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xayah.Business.Interface;
using Xayah.Business.Model;
using Xayah.Data.Context;

namespace Xayah.Data.Repository
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {

        public DocumentRepository(XayahContext context) : base(context)
        {

        }
    }
}
