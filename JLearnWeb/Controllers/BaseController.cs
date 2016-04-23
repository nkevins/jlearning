using AutoMapper;
using DAL.Repository;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace JLearnWeb.Controllers
{
    public abstract class BaseController<T, M> : Controller
        where M : new()
    {
        protected IRepository<T> _repository;

        public BaseController(IRepository<T> repository)
        {
            this._repository = repository;
        }
        
        public virtual ActionResult Index()
        {
            var entities = _repository.GetAll();
            List<M> model = new List<M>();

            foreach (var currentEntity in entities)
            {
                model.Add(Mapper.Map<M>(currentEntity));
            }

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Add()
        {
            return View(new M());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public virtual ActionResult Add(M model)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(Mapper.Map<T>(model));
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Update(int modelId)
        {
            T domainModelEntity = _repository.GetById(modelId);
            M model = Mapper.Map<M>(domainModelEntity);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public virtual ActionResult Update(M model)
        {
            if (ModelState.IsValid)
            {
                _repository.Edit(Mapper.Map<T>(model));
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public virtual ActionResult Delete(M model)
        {
            _repository.Delete(Mapper.Map<T>(model));
            return RedirectToAction("Index");
        }
    }
}