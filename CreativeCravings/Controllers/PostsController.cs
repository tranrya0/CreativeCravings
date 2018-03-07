﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CreativeCravings.DAL;
using CreativeCravings.Models;
using Microsoft.AspNet.Identity;

namespace CreativeCravings.Controllers
{
    public class PostsController : Controller
    {
        //private RecipeContext db = new RecipeContext();

        // generic repository, shares context with all other controllers
        private UnitOfWork unitOfWork = new UnitOfWork();


        public PostsController()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public PostsController(UnitOfWork uow)
        {
            this.unitOfWork = uow;
        }

        // GET: Posts
        // changed to view results so test works
        public ViewResult Index()
        {
            //return View(postRepo.Posts.ToList());
            var posts = unitOfWork.PostRepository.Get();
            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Post post = db.Posts.Find(id);
            Post post = unitOfWork.PostRepository.GetByID(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Title,Body,AuthorID")] Post post)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                // get id of current user and add it to the recipe
                var userId = User.Identity.GetUserId();
                post.AuthorID = userId;

                post.DateCreated = System.DateTime.Now;
                //db.Posts.Add(post);
                //db.SaveChanges();
                unitOfWork.PostRepository.Insert(post);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Post post = db.Posts.Find(id);
            Post post = unitOfWork.PostRepository.GetByID(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,Title,Body")] Post post)
        {
           
            if (ModelState.IsValid)
            {
                //post.DateUpdated = System.DateTime.Now;
                //db.Entry(post).State = EntityState.Modified;
                //db.SaveChanges();
                unitOfWork.PostRepository.Update(post);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Post post = db.Posts.Find(id);
            Post post = unitOfWork.PostRepository.GetByID(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Post post = db.Posts.Find(id);
            //db.Posts.Remove(post);
            //db.SaveChanges();
            Post post = unitOfWork.PostRepository.GetByID(id);
            unitOfWork.PostRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
