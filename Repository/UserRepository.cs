﻿using API.Context;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using System.Linq;

namespace Repository
{
	public class UserRepository: IUserRepository
	{
		protected EFContext _dataContext;

		public UserRepository(EFContext dataContext) 
		{  
			_dataContext = dataContext; 
		}

		public void Add(UserEntity entity) 
		{
			_dataContext.Users.Add(entity);
			_dataContext.SaveChanges();
		}

		public void Update(UserEntity entity)
		{
			_dataContext.Users.Update(entity);
			_dataContext.SaveChanges();
		}

		public UserEntity Login(UserEntity entity)
		{
			return _dataContext.Users.Include("Person").FirstOrDefault(o =>
				o.Password == entity.Password &&
				(o.Person.Email == entity.Person.Email || o.Username == entity.Username));
		}
	}
}
