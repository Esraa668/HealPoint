using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Project.Bussiness.DataTransferObjects;
using Project.Bussiness.DataTransferObjects.DepartmentDtos;
using Project.Bussiness.Services.Interfaces;
using Project.presentation.ViewModels;

namespace Project.presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _enviroment) : Controller
    {

        // BaseUrl/Department/Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();

            return View(departments);
        }

        #region Create Department

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid) //Server side validation
            {
                try
                {
                    var departmentDto = new CreatedDepartmentDto
                    {
                        Name = departmentViewModel.Name,
                        Code = departmentViewModel.Code,
                        Description = departmentViewModel.Description,
                        DateOfCreation = departmentViewModel.DateOfCreation
                    };
                    int Result = _departmentService.CreateDepartment(departmentDto);
                    string Message;
                    if (Result > 0)
                        Message = $"Department {departmentViewModel.Name} created successfully";
                    else
                        Message = $"Department {departmentViewModel.Name} can't be created";
                    TempData["Message"] = Message;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (_enviroment.IsDevelopment())
                    {
                        // 1. Development => Log error in console and return same view with error message.
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        // 2. Deployment => Log error in file | table in database and return error view.
                        _logger.LogError(ex.Message);
                    }
                }

            }
            return View(departmentViewModel);

        }


        #endregion

        #region Department details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            return View(department);
        }

        #endregion

        #region Edit Department

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(Id.Value);
            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentViewModel
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn
            };
            return View(departmentViewModel);
        }

        public IActionResult Edit([FromRoute] int? Id, DepartmentViewModel viewModel)
        {
            if (!Id.HasValue) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedDepartmentDto = new UpdatedDepartmentDto
                    {
                        Id = Id.Value,
                        Name = viewModel.Name,
                        Code = viewModel.Code,
                        Description = viewModel.Description,
                        DateOfCreation = viewModel.DateOfCreation
                    };
                    int Result = _departmentService.UpdateDepartment(updatedDepartmentDto);

                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department can't be updated");
                    }

                }
                catch (Exception ex)
                {
                    if (_enviroment.IsDevelopment())
                    {
                        // 1. Development => Log error in console and return same view with error message.
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        // 2. Deployment => Log error in file | table in database and return error view.
                        _logger.LogError(ex.Message);
                        return View("ErrorView", ex.Message);
                    }

                }
            }
            return View(viewModel);

        }
        #endregion

        #region Delete Department

        //[HttpGet]
        //public IActionResult Delete(int? Id)
        //{
        //    if (!Id.HasValue) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(Id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);
        //}
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            if (Id == 0) return BadRequest();
            try
            {
                bool Result = _departmentService.DeleteDepartment(Id);
                if (Result)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department can't be deleted");
                    return RedirectToAction(nameof(Delete), new { Id });
                }
            }
            catch (Exception ex)
            {
                if (_enviroment.IsDevelopment())
                {
                    // 1. Development => Log error in console and return same view with error message.
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // 2. Deployment => Log error in file | table in database and return error view.
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }


            }
        }

        #endregion

    }

}
