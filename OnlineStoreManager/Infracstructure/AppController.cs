using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using OnlineStoreManager.Models;

namespace OnlineStoreManager.Infracstructure
{
    public class ModelStateError
    {
        public string State { get; private set; } = "invalid";
        public string ErrorMessages { get; set; }

        public void SetState(ModelValidationState state)
        {
            switch (state)
            {
                case ModelValidationState.Invalid:
                    this.State = "invalid";
                    break;
                case ModelValidationState.Skipped:
                    this.State = "skipped";
                    break;
                case ModelValidationState.Valid:
                    this.State = "valid";
                    break;
            }
        }
    }

    public class AppController : Controller
    {
        public AppController()
        {
        }

        /* Để custom validation thôi */
        public Dictionary<string, ModelStateError> GetModelStateDictionary<TModel>()
        {
            var result = new Dictionary<string, ModelStateError>();
            foreach (var item in ModelState)
            {
                var errorMessages = item.Value.Errors
                    .Select(p => "-" + p.ErrorMessage)
                    .ToArray<string>();

                var error = new ModelStateError();
                error.ErrorMessages = String.Join("<br>", errorMessages);
                error.SetState(item.Value.ValidationState);

                result.Add(item.Key, error);
            }
            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}
