using Abeslamidze_Web.DAL.Data;
using Abeslamidze_Web.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Abeslamidze_Web.Services
{
	public class DbInitializer
	{
		public static async Task Seed(ApplicationDbContext context,
									  UserManager<ApplicationUser> userManager,
									  RoleManager<IdentityRole> roleManager)

		{
			// создать БД, если она еще не создана
			context.Database.EnsureCreated();
			// проверка наличия ролей
			if (!context.Roles.Any())
			{
				var roleAdmin = new IdentityRole
				{
					Name = "admin",
					NormalizedName = "admin"
				};
				// создать роль admin
				await roleManager.CreateAsync(roleAdmin);
			}
			// проверка наличия пользователей
			if (!context.Users.Any())
			{
				// создать пользователя user@mail.ru
				var user = new ApplicationUser
				{
					Email = "user@mail.ru",
					UserName = "user@mail.ru"
				};
				await userManager.CreateAsync(user, "123456");
				// создать пользователя admin@mail.ru
				var admin = new ApplicationUser
				{
					Email = "admin@mail.ru",
					UserName = "admin@mail.ru"
				};
				await userManager.CreateAsync(admin, "123456");
				// назначить роль admin
				admin = await userManager.FindByEmailAsync("admin@mail.ru");
				await userManager.AddToRoleAsync(admin, "admin");
			}

            //проверка наличия групп объектов
            if (!context.DishGroups.Any())
            {
                context.DishGroups.AddRange(
                new List<DishGroup>
                {
					new DishGroup { GroupName="Супы"},
					new DishGroup {GroupName="Мангал меню"},
                });
                await context.SaveChangesAsync();

            }

            // проверка наличия объектов
            if (!context.Dishes.Any())
            {
                context.Dishes.AddRange(
                new List<Dish>
                {
                    new Dish { DishName="Сырный суп с хинкали",
                    Description="Сырный суп на курином бульоне с добавлением белого вина, мини- хинкали из свинины, кинзой, перцем чили и сушеным чесноком",
                    Calories =300, DishGroupId=1, Image="cheese_soup.jpg" },
                    new Dish { DishName="Харчо",
                    Description="Острый наваристый суп с говядиной, томатной пастой, рисом, репчатым луком, пряными грузинскими специями и свежей зеленью",
                    Calories =290, DishGroupId=1, Image="kharcho_soup.jpg" },
                    new Dish {  DishName="Чихиртма",
                    Description="Куриный бульон с яичной заправкой, бедром цыпленка, чесноком, кинзой, петрушкой, винным уксусом и специями",
                    Calories =350, DishGroupId=1, Image="chikhirtma_soup.jpg" },
                    new Dish { DishName="Хачапури на мангале",
                    Description="Обжаренный на мангале сулугуни в дрожжевом тесте, с соусом дзадзики, луком, петрушкой и укропом",
                    Calories =524, DishGroupId=2, Image="khachapuri_mangal.jpg" },
                    new Dish {  DishName="Кебаб из птицы",
                    Description="Фарш из куриного филе с добавлением шпика, репчатого лука и специй, приготовлен на мангале, подаётся с капустой по-грузински",
                    Calories =180, DishGroupId=2, Image="kebab_chicken.jpg" },
                    new Dish {  DishName="Кебаб из свинины",
                    Description="Фарш из свинины с добавлением шпика, репчатого лука, специй и кинзы, приготовлен на мангале, подаётся с капустой по-грузински",
                    Calories =270, DishGroupId=2, Image="kebab_pork.jpg" },
                    new Dish {  DishName="Купаты на мангале с ткемали",
                    Description="Купаты из свинины обжаренные на гриле с соусом ткемали и капустой по-грузински",
                    Calories =560, DishGroupId=2, Image="kupaty.jpg" },
                    new Dish { DishName="Шашлык из свинины",
                    Description="Мягкая сочная свинина, маринованная в грузинских специях, обжаренная на мангале до румяной корочки, подается с маринованным луком",
                    Calories =280, DishGroupId=2, Image="shashlyk.jpg" },
                });
                await context.SaveChangesAsync();
            }
        }
	}
}
