using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ULibrary.Data;
using ULibrary.Models;

namespace ULibrary.Services;

public class SyncDbService
{
    public static async Task SyncUsersToAspNetUsersAsync(ULibraryDbContext dbContext, UserManager<User> userManager)
    {
        // Получите всех пользователей из базы данных.
        var aspNetUsers = await dbContext.Users.ToListAsync();
        var singleUsers = await dbContext.SingleUsers.ToListAsync();

        foreach (var aspUser in aspNetUsers)
        {
            var singleUser = singleUsers.FirstOrDefault(su => su.Id == int.Parse(aspUser.Id));

            // Если юзер был удален админом.
            if (singleUser == null)
            {
                dbContext.Users.Remove(aspUser);
            }
            else
            {
                // Если логин был изменен админом.
                if (aspUser.UserName != singleUser.UserName)
                    aspUser.UserName = singleUser.UserName;
                // Если изменено им и т.д.
                if (aspUser.FirstName != singleUser.FirstName)
                    aspUser.FirstName = singleUser.FirstName;
                if (aspUser.LastName != singleUser.LastName)
                    aspUser.LastName = singleUser.LastName;
                if (aspUser.Password != singleUser.Password)
                    aspUser.Password = singleUser.Password;
                if (aspUser.RoleId != singleUser.RoleId)
                    aspUser.RoleId = singleUser.RoleId;
            }
        }

        // Проверка пользователей, добавленных админом и которых нет в AspNetUsers.
        var newSingleUsers = singleUsers
            .Where(su => !aspNetUsers.Any(u => int.Parse(u.Id) == su.Id));

        foreach (var newSingleUser in newSingleUsers)
        {
            await userManager.CreateAsync(new User
            {
                Id = newSingleUser.Id.ToString(),
                UserName = newSingleUser.UserName,
                FirstName = newSingleUser.FirstName,
                LastName = newSingleUser.LastName,
                Password = newSingleUser.Password,
                RoleId = newSingleUser.RoleId
            }, newSingleUser.Password);
        }

        await dbContext.SaveChangesAsync();
    }

}
