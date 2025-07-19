using DocumentProcessor.DataLayer.Interface;
using DocumentProcessor.Model;
using DocumentProcessor.Validator.Model;
using FluentValidation;

namespace DocumentProcessor.Endpoints
{
    public static class FormEndpoint
    {
        public static void AddFormEndpoints(this WebApplication app)
        {
            app.MapGet("/form", GetForm).DisableAntiforgery();            
        }
        public static async Task<IResult> GetForm(IFormDL formDL, [AsParameters]QueryFilter<FormResponse> filter)            
        {
            var validationError = filter.Validate(typeof(FormResponse));
            if(validationError != null && validationError.Any())
            {
                return Results.BadRequest(validationError);
            }
            var response = await formDL.GetForm(filter);
            return Results.Ok(response);
        }

        public static string ValidatePostForm(Form? form, IFormFileCollection? attachments, IValidator<IFormFile> attachmentValidator)
        {
            if(form == null)
            {
                return "Form data is null";
            }
            var formValidator = new FormValidator();
            var error = formValidator.Validate(form).ToString().Split("\n")[0];
            if (error != "")
            {
                return error;
            }                               
            return string.Empty;
        }
    }
}
