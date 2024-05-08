using LXP.Common.ViewModels;
using LXP.Common.Entities;
using LXP.Core.IServices;

using LXP.Data.IRepository;
using Microsoft.AspNetCore.Http;

namespace LXP.Core.Services
{
    public class CourseServices : ICourseServices
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICourseLevelRepository _courseLevelRepository;
        

        public CourseServices(ICourseRepository courseRepository,ICategoryRepository categoryRepository,ICourseLevelRepository courseLevelRepository) 
        {
            _courseRepository = courseRepository;
            _courseLevelRepository = courseLevelRepository;
            _categoryRepository = categoryRepository;

        }
        public bool AddCourse(CourseViewModel course)
        {
            bool isCourseExists = _courseRepository.AnyCourseByCourseTitle(course.Title);
            if (!isCourseExists)
            {
                

                Guid levelId= Guid.Parse(course.Level);
                CourseLevel level = _courseLevelRepository.GetCourseLevelByCourseLevelId(levelId);
                Guid categoryId = Guid.Parse(course.Catagory);
                CourseCategory category = _categoryRepository.GetCategoryByCategoryId(categoryId);
                Course newcourse = new Course
                {
                    CourseId = Guid.NewGuid(),
                    Catagory = category,
                    Level = level,
                    Title = course.Title,
                    Description = course.Description,
                    Duration = course.Duration,
                    Thumbnail = "dbckj",
                    CreatedBy = "Admin",
                    CreatedAt = new DateTime(),
                    IsActive = true,
                    IsAvailable = true,
                    ModifiedAt = new DateTime(),
                    ModifiedBy = "Admin"


                };
                 _courseRepository.AddCourse(newcourse);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Course GetCourseByCourseId(string courseId)
        {
            Guid CourseId = Guid.Parse(courseId);
            return _courseRepository.GetCourseDetailsByCourseId(CourseId);
        }
    }
}
