using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreApiStarter.Model
{
    public interface IUserCtx
    {
        /// <summary>
        /// Obtient l'id.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Obtient le nom unique du c'est à dire le login : username ou email par ex...
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Obtient l'email.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Obtient le prénom.
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// Obtient le nom.
        /// </summary>
        string LastName { get; }
    }
}
