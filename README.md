# MovieRecommendationSystem

MovieRecommendationSystem.API Dokümantasyonu
Giriş
MovieRecommendationSystem.API, kullanıcıların film önerileri alabilmesi için geliştirilmiş bir RESTful API'dir. Bu dokümantasyon, API'nin uç noktalarını, veri formatlarını ve örnek istekleri içermektedir.

Kullanılan Teknolojiler
ASP.NET Core
MongoDB
Hangfire
JWT
RabbitMQ
CQRS
DependencyInjection
AutoMapper

Mimari
  Katmanli mimari
    API katmani:   Program.cs`te proje baslatilirken yapilmasi gereken islemleri gerceklestirir.
                   EndPointler bu katmandadir.
                   ConnectionString, Keyjwt, Api keyler gibi parametler tutulur.
                   Sadece application katmanindan referans alir.
    
    Application Katmani: Tum islemlerin yapildigi katmandir.
    Domain Katmani : Veri modellerimizin tutuldugu katmandir.
    Infrastructure katmani : Dis projeler ile ilgili islemlerin yapildigi katmandir.




Kullanıcı İşlemleri (Token gerektirmez)
1. Kullanıcı Kayıt
URL: /api/MovieRecommendationSystem/User/CreateUser
Metot: POST
Açıklama: Yeni bir kullanıcı kaydeder. Kaydedilen kullanicinin admin olup olmadigini secmelisiniz.

İstek Gövdesi:
json
{
  "name": "string",
  "surName": "string",
  "email": "string",
  "password": "string",
  "isAdmin": true,
  "createdBy": "string"
}

2. Kullanıcı Giriş
URL: /api/MovieRecommendationSystem/User/Login
Metot: POST
Açıklama: Kullanıcı girişi yapar ve JWT token döner. Bu token a gore Movie tarafinda islem yetkileriniz belirlenir.
İstek Gövdesi:
json
{
  "email": "string",
  "password": "string"
}


Film Öneri İşlemleri
1. Tum Filmleri alma
URL: /api/MovieRecommendationSystem/Movie/GetAllMovie/{pageId}&{take}
Metot: GET
Açıklama: Databasede bulunan filmeleri sayfa ve boyut bazli ceker


2. Bir filmi alma
URL: /api/MovieRecommendationSystem/Movie/GetMovie/{theMovieId}&{userId}
Metot: GET
Açıklama: Istenen filmi tum detaylari ile getiren end point


2. Filme rate verme
URL: /api/MovieRecommendationSystem/Movie/RateTheMovie
Metot: POST
Açıklama: Kullanici ve Admin secilen filme not(lar) ve rate verebilir.
İstek Gövdesi:
json
{
  "theMovieId": 0,
  "userId": "string",
  "rate": 0,
  "note": "string"
}


3. film kaydetme
URL: /api/MovieRecommendationSystem/Movie/SaveMovie
Metot: POST
Açıklama: Sadece adminler film kaydedebilir.
İstek Gövdesi:
json
{
  "moviesRequest": [
    {
      "id": 0,
      "title": "string",
      "adult": true,
      "poster_path": "string",
      "overview": "string",
      "release_date": "string",
      "genre_ids": [
        0
      ],
      "original_title": "string",
      "original_language": "string",
      "backdrop_path": "string",
      "popularity": 0,
      "vote_count": 0,
      "video": true,
      "vote_average": 0
    }
  ]
}


2. film onerme
URL: /api/MovieRecommendationSystem/Movie/RecommendTheMovie
Metot: POST
Açıklama: Kullanici ya da admin secilen bir filmi istenen bir e postaya mail atarak onerebilir. Mail donusu alttadir.
İstek Gövdesi:
json
{
  "theMovieId": 0,
  "emailAddress": "string"
}
![image](https://github.com/Ogulcan-Dinsever/MovieRecommendationSystem/assets/67371554/309e5194-8bd0-4f47-8e10-59bad08ccca2)


RABBIT MQ 
/api/MovieRecommendationSystem/Movie/RecommendTheMovie adresine bir istek atildiginda bir mail bodysi olusturulur. Bu mail bir kuyruga atilir. Consumer dinler ve mesaji alir. Gerekli mail atma islemini gerceklestirir.
