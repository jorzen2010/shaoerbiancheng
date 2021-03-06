﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SkyEntity
{
    public class VideoCourse
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "标题")]
        [Required(ErrorMessage = "请输入标题")]

        public string Title { get; set; }

        [Required(ErrorMessage = "请输入摘要")]
        [Display(Name = "摘要")]
        public string Info { get; set; }

        [Required(ErrorMessage = "请输入内容")]
        [Display(Name = "内容")]
        public string Content { get; set; }


        [Display(Name = "标签")]
        public string Tags { get; set; }

        [Display(Name = "封面")]
        public string Cover { get; set; }

        [Display(Name = "类别")]
        [Required(ErrorMessage = "类别不能可为空")]
        public int Category { get; set; }

        [Display(Name = "作者")]
        public string Author { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
        

        [Display(Name = "视频ID")]
        [Required(ErrorMessage = "视频ID不能可为空")]
        public string VideoId { get; set; }

        [Display(Name = "排序ID")]
        public int Paixu { get; set; }



    }
}
