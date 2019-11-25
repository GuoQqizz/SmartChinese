using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity.Subject;
using System;

namespace AbhsChinese.Admin.Models.Common
{
    public abstract class ContentFetcher
    {
        /// <summary>
        /// 获取Yw_SubjectContent对象,以及对对象如何操作(添加/更新)
        /// 用于向数据库添加或更新数据
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="bll"></param>
        /// <param name="currentUser"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Action<Yw_SubjectContent> Fetch(
            QuestionInputModel subject,
            SubjectBll bll,
            int currentUser,
            out Yw_SubjectContent entity)
        {
            Check.IfNull(subject, nameof(subject));
            if (currentUser < 10000)
            {
                throw new ArgumentException(
                    "当前登录用户的ID不能小于10000",
                    nameof(currentUser));
            }

            Action<Yw_SubjectContent> saveContentMethod = null;
            Yw_SubjectContent contentInDb = bll.GetSubjectContent(subject.Id);
            if (contentInDb == null)
            {
                saveContentMethod = bll.InsertContent;
            }
            else
            {
                saveContentMethod = bll.UpdateContent;
            }
            Yw_SubjectContent content = GetContent(subject, bll, currentUser, contentInDb);
            entity = content;
            return saveContentMethod;
        }

        /// <summary>
        /// 生成一个Subject Content对象,该对象用于向数据库中添加/更新数据
        /// </summary>
        /// <param name="sub">封装着前端传来的数据</param>
        /// <param name="bll">SubjectBll对象</param>
        /// <param name="currentUser">当前用户</param>
        /// <param name="content">Subject Content对象,该对象从数据库中查询所得,用于判断是添加还是更新操作</param>
        /// <returns></returns>
        protected abstract Yw_SubjectContent GetContent(
            QuestionInputModel sub,
            SubjectBll bll,
            int currentUser,
            Yw_SubjectContent content);
    }
}