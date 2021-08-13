using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunlerController : Controller
    {
        // GET: Urunler

        MvcDbStokEntities db = new MvcDbStokEntities();

        public ActionResult Index()
        {
            var degerler = db.TblUrunler.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList() 
                                             select new SelectListItem 
                                             { Text = i.KategoriAd, Value = i.KategoriId.ToString() }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(TblUrunler p1)
        {
            var ktg = db.TblKategoriler.Where(m => m.KategoriId == p1.TblKategoriler.KategoriId).FirstOrDefault();
            p1.TblKategoriler = ktg;
            db.TblUrunler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var urun = db.TblUrunler.Find(id);
            db.TblUrunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.TblUrunler.Find(id);

            List<SelectListItem> degerler = (from i in db.TblKategoriler.ToList()
                                             select new SelectListItem
                                             { Text = i.KategoriAd, Value = i.KategoriId.ToString() }).ToList();
            ViewBag.dgr = degerler;

            return View("UrunGetir", urun);
        }

        public ActionResult Guncelle(TblUrunler p1)
        {
            var urun = db.TblUrunler.Find(p1.UrunId);
            urun.UrunAd = p1.UrunAd;
            urun.Marka = p1.Marka;
            urun.Stok = p1.Stok;
            urun.Fiyat = p1.Fiyat;
            //urun.UrunKategori = p1.UrunKategori;
            var ktg = db.TblKategoriler.Where(m => m.KategoriId == p1.TblKategoriler.KategoriId).FirstOrDefault();
            urun.UrunKategori = ktg.KategoriId;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}