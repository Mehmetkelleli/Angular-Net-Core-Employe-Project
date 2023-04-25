using Backend.Application.Dtos;
using Backend.Application.Exceptions;
using Backend.Domain.EntityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Features.Employe.Query.GetList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQueryRequest, GetUserListQueryResponse>
    {
        private RoleManager<Domain.EntityModels.Role> _Role;
        private UserManager<Domain.EntityModels.Employe> _Employe;

        public GetUserListQueryHandler(RoleManager<Domain.EntityModels.Role> role, UserManager<Domain.EntityModels.Employe> employe)
        {
            _Role = role;
            _Employe = employe;
        }

        public async Task<GetUserListQueryResponse> Handle(GetUserListQueryRequest request, CancellationToken cancellationToken)
        {
            var employes = _Employe.Users.Include(i => i.PersonelTasks);
            var employeList = new List<EmployeWithWage>();
            if(string.IsNullOrEmpty(request.UserId))
            {
                foreach (var employe in employes)
                {
                    foreach (var role in _Role.Roles)
                    {
                        if (await _Employe.IsInRoleAsync(employe, role.Name))
                        {
                            if (role.Name != "Admin")
                            {
                                var EmpDto = new EmployeWithWage();
                                EmpDto.Employe = employe;
                                EmpDto.TotalWage = (employe.PersonelTasks.Where(i => i.State == false).Count() * role.Wage) + (employe.PersonelTasks.Where(i => i.WageHourState == true &&!i.State).Select(i => i.WageHours).Sum() * role.OvertimePay);

                                EmpDto.TotalHours = employe.PersonelTasks.Where(i => i.WageHourState == false).Select(i => i.WageHours).Sum();

                                EmpDto.Roles = (await _Employe.GetRolesAsync(employe)).ToArray();

                                employeList.Add(EmpDto);
                            }
                        }
                    }

                }
            }
            else
            {
                var user = employes.FirstOrDefault(i => i.Id == request.UserId);
                if(user == null)
                {
                    throw new CustomException(new string[] { "user-not-found" });
                }
                foreach (var role in _Role.Roles)
                {
                    if (await _Employe.IsInRoleAsync(user, role.Name))
                    {
                        if (role.Name != "Admin")
                        {
                            var EmpDto = new EmployeWithWage();
                            EmpDto.Employe = user;
                            EmpDto.TotalWage = (user.PersonelTasks.Where(i => i.State == false).Count() * role.Wage) + (user.PersonelTasks.Where(i => i.WageHourState == true && !i.State).Select(i => i.WageHours).Sum() * role.OvertimePay);

                            EmpDto.TotalHours = user.PersonelTasks.Where(i => i.WageHourState == false).Select(i => i.WageHours).Sum();

                            EmpDto.TotalPrice = (user.PersonelTasks.Where(i => i.State == true).Count() * role.Wage) + (user.PersonelTasks.Where(i => i.WageHourState == true && i.State).Select(i => i.WageHours).Sum() * role.OvertimePay);

                            EmpDto.TotalPriceHour = user.PersonelTasks.Where(i => i.WageHourState == true && i.State==true).Select(i => i.WageHours).Sum();

                            EmpDto.Roles = (await _Employe.GetRolesAsync(user)).ToArray();
                            employeList.Add(EmpDto);
                        }
                    }
                }

            }
            var model = new GetUserListQueryResponse();
            model.EmployeWithWages = employeList.AsQueryable();
            return model;
        }

    }
}
