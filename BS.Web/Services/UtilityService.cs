namespace BS.Web.Services
{
    public class UtilityService
    {
        public static object GET_MODEL_ERRORS_OBJECT(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary ModelState)
        {
            var controls = ModelState.Where(m => m.Value!.Errors.Count > 0)
                                   .Select(m => new ErrorModel
                                   {
                                       NAME = m.Key,
                                       ERROR = string.Join(" | ", m.Value!.Errors.Select(e => e.ErrorMessage))
                                   })
                                   .ToList();

            return new { Success = false, Message = string.Join(" | ", controls.Select(x => x.ERROR)), Controls = controls };
        }
        public static string GET_MODEL_ERRORS(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary ModelState)
        {
            var controls = ModelState.Where(m => m.Value!.Errors.Count > 0)
                                   .Select(m => new ErrorModel
                                   {
                                       NAME = m.Key,
                                       ERROR = string.Join(" | ", m.Value!.Errors.Select(e => e.ErrorMessage))
                                   })
                                   .ToList();

            return string.Join(" | ", controls.Select(x => x.ERROR));
        }
    }
}
