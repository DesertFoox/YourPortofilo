using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Portofilo
{
    public class PortofiloSql : IPortofiloRepo
    {
        private readonly IAppDBContext _context;
        public PortofiloSql(IAppDBContext context)
        {
            _context = context;
        }
        public PortoFilo CreatePortofilo(PortoFilo porto)
        {
            var NewSkill = _context.PortoFilos.Add(porto);
            SaveChanges();
            return _context.PortoFilos.FirstOrDefault(x => x.Id == porto.Id);
        }

        public void DeletePortoFilo(Guid id)
        {
            var Porto = _context.PortoFilos.FirstOrDefault(x => x.Id == id);
            _context.PortoFilos.Remove(Porto);
            SaveChanges();
        }

        public PortoFilo GetPortoFiloById(Guid id)
        {
            return _context.PortoFilos.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<PortoFilo> GetUserPortofiloById(Guid id)
        {
            return _context.PortoFilos.Where(x => x.UserId == id).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public PortoFilo UpdatePortofilo( PortoFilo porto)
        {
            var OldPorto = _context.PortoFilos.FirstOrDefault(x => x.Id == porto.Id);
            OldPorto.Title = porto.Title;
            OldPorto.Description = porto.Description;
            OldPorto.Image = porto.Image;
            SaveChanges();
            return OldPorto;
        }

        public void UploadImage(Guid id, string imagepath)
        {
            var Porto = _context.PortoFilos.FirstOrDefault(x=>x.Id == id);
            Porto.Image = imagepath;
            SaveChanges();
        }
    }
}
