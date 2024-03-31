using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Enums
{
    public enum StatusCode
    {
        Ok = 200,//Все хорошо
        NotFound = 404,//Страница не найдена
        InternalServerError = 500//Внутренняя ошибка сервера
    }
}
