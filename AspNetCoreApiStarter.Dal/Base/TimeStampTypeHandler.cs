using AspNetCoreApiStarter.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AspNetCoreApiStarter.Dal.Base
{
    /// <summary>
    /// Un mapper dapper pour gérer simplement pour le type <see cref="TimeStamp"/>.
    /// Permet de mapper le type timestamp sql server.
    /// </summary>
    public class TimeStampTypeHandler : SqlMapper.TypeHandler<TimeStamp>
    {
        /// <summary>
        /// Obtenir un objet de type timestamp depuis un champ de base de donnée.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override TimeStamp Parse(object value)
        {
            Array.Reverse((byte[])value); // passage big endian => little endian
            return new TimeStamp((byte[])value);
        }

        /// <summary>
        /// Ecriture des données du type timestamp dans un champs de la base de donnée.
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="value"></param>
        public override void SetValue(IDbDataParameter parameter, TimeStamp ts)
        {
            Array.Reverse(ts.Value); // passage little endian => big endian
            parameter.Value = ts.Value;
        }
    }
}