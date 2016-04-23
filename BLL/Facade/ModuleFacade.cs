using DAL.Repository;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public class ModuleFacade
    {
        private IUnitOfWork _uow;

        public ModuleFacade()
        {
            _uow = new UnitOfWork();
        }

        public ModuleFacade(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<Module> GetBySchedule(int scheduleId)
        {
            return _uow.ModuleRepository.GetAll().Where(x => x.ScheduleID == scheduleId && x.ObsInd == "N").OrderBy(x => x.ModuleName).ToList();
        }

        public void Add(Module module)
        {
            _uow.ModuleRepository.Insert(module);
            _uow.Save();
        }
    }
}
