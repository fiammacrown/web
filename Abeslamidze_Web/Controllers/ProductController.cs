using Abeslamidze_Web.DAL.Entities;
using Abeslamidze_Web.Extensions;
using Abeslamidze_Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Abeslamidze_Web.Controllers
{
    public class ProductController : Controller
    {
        public List<Dish> _dishes;
        List<DishGroup> _dishGroups;

        int _pageSize;

        public ProductController()
        {
            _pageSize = 3;
            SetupData();
        }

        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        public IActionResult Index(int? group, int pageNo = 1)
        {
            var dishesFiltered = _dishes.Where(d => !group.HasValue || d.DishGroupId == group.Value);

            // Поместить список групп во ViewData
            ViewData["Groups"] = _dishGroups;
            // Получить id текущей группы и поместить в TempData
            ViewData["CurrentGroup"] = group ?? 0;
			var model = ListViewModel<Dish>.GetModel(dishesFiltered, pageNo, _pageSize);
			if (RequestExtensions.IsAjaxRequest(Request))
                return PartialView("_listpartial", model);
			else
				return View(model);
		}

        /// <summary>
        /// Инициализация списков
        /// </summary>
        private void SetupData()
        {
            _dishGroups = new List<DishGroup>{
                new DishGroup {DishGroupId=1, GroupName="Супы"},
                new DishGroup {DishGroupId=2, GroupName="Мангал меню"},
            };
            _dishes = new List<Dish>{
                new Dish {DishId = 1, DishName="Сырный суп с хинкали",
                Description="Сырный суп на курином бульоне с добавлением белого вина, мини- хинкали из свинины, кинзой, перцем чили и сушеным чесноком",
                Calories =300, DishGroupId=1, Image="cheese_soup.jpg" },
                new Dish { DishId = 2, DishName="Харчо",
                Description="Острый наваристый суп с говядиной, томатной пастой, рисом, репчатым луком, пряными грузинскими специями и свежей зеленью",
                Calories =290, DishGroupId=1, Image="kharcho_soup.jpg" },
                new Dish { DishId = 3, DishName="Чихиртма",
                Description="Куриный бульон с яичной заправкой, бедром цыпленка, чесноком, кинзой, петрушкой, винным уксусом и специями",
                Calories =350, DishGroupId=1, Image="chikhirtma_soup.jpg" },
                new Dish { DishId = 4, DishName="Хачапури на мангале",
                Description="Обжаренный на мангале сулугуни в дрожжевом тесте, с соусом дзадзики, луком, петрушкой и укропом",
                Calories =524, DishGroupId=2, Image="khachapuri_mangal.jpg" },
                new Dish { DishId = 5, DishName="Кебаб из птицы",
                Description="Фарш из куриного филе с добавлением шпика, репчатого лука и специй, приготовлен на мангале, подаётся с капустой по-грузински",
                Calories =180, DishGroupId=2, Image="kebab_chicken.jpg" },
                new Dish { DishId = 6, DishName="Кебаб из свинины",
                Description="Фарш из свинины с добавлением шпика, репчатого лука, специй и кинзы, приготовлен на мангале, подаётся с капустой по-грузински",
                Calories =270, DishGroupId=2, Image="kebab_pork.jpg" },
                new Dish { DishId = 7, DishName="Купаты на мангале с ткемали",
                Description="Купаты из свинины обжаренные на гриле с соусом ткемали и капустой по-грузински",
                Calories =560, DishGroupId=2, Image="kupaty.jpg" },
                new Dish { DishId = 8, DishName="Шашлык из свинины",
                Description="Мягкая сочная свинина, маринованная в грузинских специях, обжаренная на мангале до румяной корочки, подается с маринованным луком",
                Calories =280, DishGroupId=2, Image="shashlyk.jpg" },
            };
        }
    }
}
