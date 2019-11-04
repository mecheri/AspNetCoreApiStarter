using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApiStarter.Bll.Itf.Bll;
using AspNetCoreApiStarter.Controllers.Core;
using AspNetCoreApiStarter.Model;
using AspNetCoreApiStarter.Resources;
using AspNetCoreApiStarter.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NSwag.Annotations;

namespace AspNetCoreApiStarter.Controllers
{
    /// <summary>
    /// User Controller.
    /// </summary>
    // [Authorize(Policy = "ApiUser")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserBll userBll;
        private readonly IStringLocalizer<SharedResources> localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userBll">User services.</param>
        /// <param name="localizer">Localizer services.</param>
        public UsersController(IUserBll userBll, IStringLocalizer<SharedResources> localizer)
        {
            this.userBll = userBll;
            this.localizer = localizer;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>User dto.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserVm>>> Get()
        {
            var users = await this.userBll.Get();
            IEnumerable<UserVm> usersVm = users.Select(user => UserVm.Load(user)).ToList();
            return this.Ok(usersVm);
        }

        /// <summary>
        /// Gets a specific user.
        /// </summary>
        /// <param name="id">User Id.</param>
        /// <returns>User DTO. Http 200 if Ok.</returns>
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserVm>> Get(int id)
        {
            User user = await this.userBll.Get(id);
            if (user == null)
            {
                return this.NotFound();
            }

            return this.Ok(UserVm.Load(user));
        }

        /// <summary>
        /// Creates a new User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /User
        ///     {
        ///        "username": "test",
        ///        "email": "test@test.com",
        ///        "password": "password",
        ///        "firstName": "test",
        ///        "lastName": "test"
        ///     }.
        ///
        /// </remarks>
        /// <param name="vm">User VM.</param>
        /// <returns>A newly-created User.</returns>
        /// <response code="201">Returns the newly-created item.</response>
        /// <response code="400">If the item is null.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(typeof(UserVm))]
        public async Task<ActionResult<UserVm>> Post([FromBody] UserVm vm)
        {
            if (vm == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return new ValidationFailedResult(this.ModelState);
            }

            User newUser = await this.userBll.Create(UserVm.Get(vm));
            UserVm newUserVm = UserVm.Load(newUser);

            return this.CreatedAtRoute("GetUser", new { id = newUserVm.Id }, newUserVm);
        }

        /// <summary>
        /// Updates a specific user.
        /// </summary>
        /// <param name="id">User Id.</param>
        /// <param name="vm">User VM.</param>
        /// <returns>Updated user DTO. Http 200 if Ok.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(typeof(UserVm))]
        public async Task<ActionResult<UserVm>> Put(int id, [FromBody] UserVm vm)
        {
            if (vm == null || vm.Id != id)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return new ValidationFailedResult(this.ModelState);
            }

            User user = UserVm.Get(vm);
            if (user == null)
            {
                return this.NotFound();
            }

            User updatedUser = await this.userBll.Update(user);
            return this.Ok(UserVm.Load(updatedUser));
        }

        /// <summary>
        /// Deletes a specific user.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>Http 200 if ok.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool ok = await this.userBll.Delete(id);
            if (!ok)
            {
                return this.NotFound();
            }

            return this.Ok();
        }
    }
}
