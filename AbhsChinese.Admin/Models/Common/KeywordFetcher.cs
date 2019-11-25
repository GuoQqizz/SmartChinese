using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbhsChinese.Admin.Models.Common
{
    public static class KeywordFetcher
    {
        public static IList<Yw_SubjectIndex> Fetch(
            QuestionInputModel subject,
            SubjectBll bll,
            int currentUser,
            out IList<int> toDelete)
        {
            Check.IfNull(subject, nameof(subject));
            Check.IfNull(bll, nameof(bll));
            if (currentUser < 10000)
            {
                throw new ArgumentException(
                    "当前登录用户的ID不能小于10000",
                    nameof(currentUser));
            }
            DateTime now = DateTime.Now;
            //前端传值过来的关键词
            IList<string> keywords = new List<string>();
            if (!string.IsNullOrWhiteSpace(subject.Keywords))
            {
                keywords = subject.Keywords.Split(',');
            }
            
            //数据库中的关键词
            IEnumerable<Yw_SubjectIndex> keywordEntities =
                bll.GetKeywordsBySubject(subject.Id);
            //要删除的关键词
            IList<int> keywordIdsToDelete = null;
            //要添加的关键词
            IList<Yw_SubjectIndex> keywordsToAdd = null;
            IEnumerable<string> keywordsInDb =
                    keywordEntities.Select(k => k.Ysi_Keyword);
            IEnumerable<string> keywordsToDelete = keywordsInDb.Except(keywords);
            keywordIdsToDelete =
                    keywordEntities.Where(k => keywordsToDelete.Contains(k.Ysi_Keyword))
                                   .Select(k => k.Ysi_Id)
                                   .ToList();
            keywordsToAdd = keywords.Except(keywordsInDb)
                                        .Select(k => new Yw_SubjectIndex
                                        {
                                            Ysi_CreateTime = now,
                                            Ysi_Creator = currentUser,
                                            Ysi_Difficulty = subject.Difficulty,
                                            Ysi_Id = 0,
                                            Ysi_Keyword = k
                                        }).ToList();

            toDelete = keywordIdsToDelete;
            return keywordsToAdd;
        }
    }
}