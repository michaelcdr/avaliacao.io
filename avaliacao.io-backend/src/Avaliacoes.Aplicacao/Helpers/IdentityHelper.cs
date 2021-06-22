using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacoes.Aplicacao.Helpers
{
    public class IdentityHelper
    {
        public static List<string> ObterErros(IdentityResult identityResult)
        {
            var erros = new List<string>();
            foreach (var item in identityResult.Errors)
                if ("PasswordMismatch" == item.Code)
                    erros.Add("As senhas não conferem.");
                else if ("DuplicateUserName" == item.Code)
                    erros.Add("O usuário informado está em uso.");
                else
                    erros.Add($"{item.Code} - {item.Description}");
            return erros;
        }
    }
}
