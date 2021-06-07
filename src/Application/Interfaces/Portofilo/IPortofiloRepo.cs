using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPortofiloRepo
    {
        bool SaveChanges();
        public IEnumerable<PortoFilo> GetUserPortofiloById(Guid id);
        public PortoFilo CreatePortofilo( PortoFilo porto);
        public void DeletePortoFilo(Guid id);
        public PortoFilo UpdatePortofilo(PortoFilo porto);
        public PortoFilo GetPortoFiloById(Guid id);
        public void UploadImage(Guid id,string imagepath);
    }
}
