const books = [
    {
        id: 1,
        image: "https://img.taaghche.com/frontCover/69253.jpg?w=150",
        title: "هستی",
        author: "فرهاد حسن زاده",
        publisher: "کانون پرورش فکری کودکان و نوجوانان",
        categories: [
            "داستان کودک و نوجوانان",
            "داستان ایرانی",
            "مجموعه کتاب باز",
            "باشگاه کتاب‌خوانی ازتا",
            "کمپین دنیای قشنگ تو - ۹۹"
        ],
        rating: 4.6,
        votes: 489,
        price: "120,000 تومان"
    },
    {
        id: 2,
        title: "موش سر به هوا",
        image: "https://img.taaghche.com/frontCover/69258.jpg?w=150",
        author: "فرهاد حسن زاده",
        publisher: "کانون پرورش فکری کودکان و نوجوانان",
        categories: [
            "داستان کودک و نوجوانان",
            "کمپین دنیای قشنگ تو - ۹۸",
            "وینچنزو",
            "کمپین دنیای قشنگ تو - ۹۹"
        ],
        rating: 4.1,
        votes: 549,
        price: "120,000 تومان"
    },
    {
        id: 3,
        image: "https://img.taaghche.com/frontCover/85823.jpg?w=150",
        title: "جنگی که بالاخره نجاتم داد",
        author: "کیمبِرلی بروبیکر بردلی",
        translator: "مرضیه ورشوساز",
        publisher: "انتشارات پرتقال",
        categories: [
            "رمان",
            "داستان کودک و نوجوانان"
        ],
        rating: 4.6,
        votes: 130,
        price: "120,000 تومان"
    },
    {
        id: 4,
        image: "https://img.taaghche.com/frontCover/66612.jpg?w=150",
        title: "ماهی سیاه کوچولو",
        author: "بهرنگ .",
        publisher: "کانون پرورش فکری کودکان و نوجوانان",
        categories: [
            "داستان کودک و نوجوانان",
            "داستان ایرانی",
            "کمپین دنیای قشنگ تو - ۹۸",
            "کمپین دنیای قشنگ تو - ۹۹"
        ],
        rating: 4.2,
        votes: 165,
        price: "120,000 تومان"
    },
    {
        id: 5,
        image: "https://img.taaghche.com/frontCover/69256.jpg?w=150",
        title: "هیچ کس جرئتش را ندارد",
        author: "حمیدرضا شاه‌آبادی",
        publisher: "کانون پرورش فکری کودکان و نوجوانان",
        categories: [
            "داستان کودک و نوجوانان",
            "داستان ایرانی",
            "کمپین دنیای قشنگ تو - ۹۸",
            "وینچنزو",
            "کمپین دنیای قشنگ تو - ۹۹"
        ],
        rating: 4.2,
        votes: 112,
        price: "120,000 تومان"
    },
    {
        id: 6,
        image: "https://img.taaghche.com/frontCover/93549.jpg?w=150",
        title: "ساداکو و هزار درنای کاغذی",
        author: "الینور کوئر",
        translator: "مریم پیشگاه",
        publisher: "کانون پرورش فکری کودکان و نوجوانان",
        categories: [
            "داستان کودک و نوجوانان"
        ],
        rating: 4.4,
        votes: 91,
        price: "120,000 تومان"
    },
    {
        id: 7,
        image: "https://img.taaghche.com/frontCover/64194.jpg?w=150",
        title: "دشمن",
        author: "دیوید کالی",
        translator: "رضی هیرمندی",
        publisher: "نشر کتاب چ",
        categories: [
            "داستان کودک و نوجوانان",
            "کمپین دنیای قشنگ تو - ۹۸",
            "کمپین دنیای قشنگ تو - ۹۹"
        ],
        rating: 4.3,
        votes: 89,
        price: "120,000 تومان"
    },
    {
        id: 8,
        image: "https://img.taaghche.com/frontCover/87400.jpg?w=150",
        title: "روباهی به نام پکس",
        author: "سارا پنی‌پکر",
        translator: "بهرنگ خسروی",
        publisher: "انتشارات پرتقال",
        categories: [
            "داستان کودک و نوجوانان"
        ],
        rating: 4.5,
        votes: 76,
        price: "120,000 تومان"
    },
    {
        id: 9,
        image: "https://img.taaghche.com/frontCover/126588.jpg?w=150",
        title: "ته کلاس، ردیف آخر، صندلی آخر",
        author: "لوییس سکر",
        translator: "پروین علی پور",
        publisher: "نشر افق",
        categories: [
            "داستان کودک و نوجوانان",
            "مجموعه رمان نوجوان"
        ],
        rating: 4.7,
        votes: 72,
        price: "120,000 تومان"
    },
    {
        id: 10,
        image: "https://img.taaghche.com/frontCover/83665.jpg?w=150",
        title: "اکو؛ جلد اول",
        author: "پم مونیوس رایان",
        translator: "فروغ منصورقناعی",
        publisher: "انتشارات پرتقال",
        categories: [
            "داستان کودک و نوجوانان",
            "مجموعه اکو"
        ],
        rating: 4.7,
        votes: 69,
        price: "120,000 تومان"
    },
    {
        id: 11,
        image: "https://img.taaghche.com/audioCover/63138.jpg?w=150",
        title: "کودک، سرباز و دریا (کتاب صوتی)",
        author: "ژرژ فون ویلیه",
        translator: "دل‌آرا قهرمان",
        narrator: "امین زندگانی",
        publisher: "کانون پرورش فکری کودکان و نوجوانان",
        categories: [
            "رمان",
            "داستان خارجی",
            "داستان کودک و نوجوانان",
            "مجموعه کتاب باز",
            "کمپین دنیای قشنگ تو - ۹۸",
            "کمپین دنیای قشنگ تو - ۹۹"
        ],
        rating: 4.3,
        votes: 87,
        price: "120,000 تومان"
    },
    {
        "id": 12,
        "image": "https://img.taaghche.com/audioCover/225149.jpg?w=150",
        "title": "آخرین کتابفروشی لندن (کتاب صوتی)",
        "author": "مادلین مارتین",
        "translator": "مریم فرهمندپور",
        "narrator": "مرجان شهنازفرد",
        "publisher": "انتشارات دیجیتال آوای موج کتاب (نشر صوتی)",
        "categories": [
            "رمان",
            "داستان تاریخی",
            "داستان خارجی"
        ],
        "rating": 3.9,
        "votes": 73,
        "duration": "12 ساعت و 40 دقیقه",
        "size": "1 گیگابایت, 20.2 مگابایت",
        "transferable": false,
        "price": "39,000 تومان",
        "originalPrice": "130,000 تومان",
        "discount": "70%"
    },
    {
        "id": 13,
        "image": "https://img.taaghche.com/audioCover/223881.jpg?w=150",
        "title": "بیگانه‌ای بر لب رودخانه (کتاب صوتی)",
        "author": "پال توئیچل",
        "translator": "هوشنگ اهرپور",
        "narrator": "رضا خضرایی",
        "publisher": "نوین کتاب گویا",
        "categories": [
            "داستان خارجی",
            "موفقیت و خودیاری"
        ],
        "rating": 3.5,
        "votes": 107,
        "duration": "5 ساعت و 13 دقیقه",
        "size": "304.7 مگابایت",
        "transferable": false,
        "price": "42,000 تومان",
        "originalPrice": "140,000 تومان",
        "discount": "70%"
    },
    {
        "id": 14,
        "image": "https://img.taaghche.com/audioCover/191890.jpg?w=150",
        "title": "هومان (کتاب صوتی)",
        "author": "محمد نصراوی",
        "narrator": "میلاد مبینی",
        "publisher": "نشر سماوا",
        "categories": [
            "علمی تخیلی",
            "داستان تاریخی",
            "داستان کودک و نوجوانان"
        ],
        "rating": 4.0,
        "votes": 26,
        "duration": "4 ساعت و 41 دقیقه",
        "size": "645.5 مگابایت",
        "transferable": true,
        "price": "63,000 تومان",
        "originalPrice": "90,000 تومان",
        "discount": "30%"
    },
    {
        "id": 15,
        "image": "https://img.taaghche.com/audioCover/219179.jpg?w=150",
        "title": "شراره‌های آتش (کتاب صوتی)",
        "author": "علی اکبر والایی",
        "narrator": "شراره حضرتی",
        "publisher": "نشر سماوا",
        "categories": [
            "داستان ایرانی"
        ],
        "rating": 3.7,
        "votes": 24,
        "duration": "5 ساعت و 41 دقیقه",
        "size": "782.1 مگابایت",
        "transferable": true,
        "price": "49,000 تومان",
        "originalPrice": "70,000 تومان",
        "discount": "30%"
    },
    {
        "id": 16,
        "image": "https://img.taaghche.com/audioCover/204150.jpg?w=150",
        "title": "مثل دست‌های مادرم (کتاب صوتی)",
        "author": "خسرو باباخانی",
        "narrator": "بهروز رضوی",
        "publisher": "نشر سماوا",
        "categories": [
            "داستان کودک و نوجوانان"
        ],
        "rating": 3.8,
        "votes": 31,
        "duration": "3 ساعت و 28 دقیقه",
        "size": "286.1 مگابایت",
        "transferable": true,
        "price": "21,000 تومان",
        "originalPrice": "30,000 تومان",
        "discount": "30%"
    },
    {
        "id": 17,
        "image": "https://img.taaghche.com/audioCover/59164.jpg?w=150",
        "title": "مغازه خودکشی (کتاب صوتی)",
        "author": "ژان تولی",
        "translator": "احسان کرم‌ویسی",
        "narrator": "هوتن شکیبا",
        "publisher": "رادیو گوشه",
        "categories": [
            "رمان",
            "داستان خارجی",
            "طنز",
            "فانتزی"
        ],
        "rating": 4.0,
        "votes": 5327,
        "duration": "3 ساعت و 6 دقیقه",
        "size": "440.8 مگابایت",
        "transferable": false,
        "price": "55,800 تومان",
        "originalPrice": "93,000 تومان",
        "discount": "40%"
    },
    {
        "id": 18,
        "image": "https://img.taaghche.com/audioCover/165476.jpg?w=150",
        "title": "اول عاشق خودت باش (کتاب صوتی)",
        "author": "مارک رکلاو",
        "translator": "زهرا حق پرست آزاد",
        "narrator": "وحید مرتضوی کیاسری",
        "publisher": "شادن پژواک",
        "categories": [
            "موفقیت و خودیاری"
        ],
        "rating": 4.1,
        "votes": 1545,
        "duration": "4 ساعت و 48 دقیقه",
        "size": "264.0 مگابایت",
        "transferable": true,
        "price": "29,000 تومان",
        "originalPrice": "58,000 تومان",
        "discount": "50%"
    },
    {
        "id": 19,
        "image": "https://img.taaghche.com/audioCover/187855.jpg?w=150",
        "title": "خاطرات یک آدم کش (کتاب صوتی)",
        "author": "کیم یونگ ها",
        "translator": "خاطره کردکریمی",
        "narrator": "هوتن شکیبا",
        "publisher": "رادیو گوشه",
        "categories": [
            "رمان",
            "داستان خارجی",
            "وحشت",
            "جنایی"
        ],
        "rating": 4.0,
        "votes": 1145,
        "duration": "3 ساعت و 20 دقیقه",
        "size": "458.6 مگابایت",
        "transferable": false,
        "price": "60,000 تومان",
        "originalPrice": "100,000 تومان",
        "discount": "40%"
    },
    {
        "id": 20,
        "image": "https://img.taaghche.com/audioCover/119098.jpg?w=150",
        "title": "هنر ظریف بیخیالی (کتاب صوتی)",
        "author": "مارک منسون",
        "translator": "گروه مترجمان نشر مسیر سبز رشد",
        "narrator": "لیا عرفانی",
        "publisher": "نشر مسیر سبز رشد",
        "categories": [
            "موفقیت و خودیاری",
            "روانشناسی"
        ],
        "rating": 3.6,
        "votes": 854,
        "duration": "2 ساعت و 24 دقیقه",
        "size": "330.5 مگابایت",
        "transferable": false,
        "price": "12,500 تومان",
        "originalPrice": "25,000 تومان",
        "discount": "50%"
    },
    {
        "id": 21,
        "image": "https://img.taaghche.com/audioCover/123153.jpg?w=150",
        "title": "قورباغه ات را قورت بده! (کتاب صوتی)",
        "author": "برایان تریسی",
        "translator": "گروه مترجمان نشر مسیر سبز رشد",
        "narrator": "مجید قائدی",
        "publisher": "نشر مسیر سبز رشد",
        "categories": [
            "موفقیت و خودیاری",
            "کسب و کار"
        ],
        "rating": 4.2,
        "votes": 1789,
        "duration": "1 ساعت و 50 دقیقه",
        "size": "252.9 مگابایت",
        "transferable": false,
        "price": "12,500 تومان",
        "originalPrice": "25,000 تومان",
        "discount": "50%"
    },
    {
        "id": 22,
        "image": "https://img.taaghche.com/audioCover/81647.jpg?w=150",
        "title": "تکنیک‌های تسلط بر ذهن (کتاب صوتی)",
        "author": "آنتونی رابینز",
        "translator": "سعید ابراهیم پور",
        "narrator": "لیا عرفانی",
        "publisher": "نشر مسیر سبز رشد",
        "categories": [
            "موفقیت و خودیاری",
            "کسب و کار"
        ],
        "rating": 3.7,
        "votes": 2767,
        "duration": "53 دقیقه",
        "size": "121.7 مگابایت",
        "transferable": false,
        "price": "12,500 تومان",
        "originalPrice": "25,000 تومان",
        "discount": "50%"
    },
    {
        "id": 23,
        "image": "https://img.taaghche.com/audioCover/222186.jpg?w=150",
        "title": "سووشون (کتاب صوتی)",
        "author": "سیمین دانشور",
        "narrator": "شیما درخشش، سحر بیرانوند و جمعی از گویندگان",
        "publisher": "آوانامه",
        "categories": [
            "رمان",
            "داستان ایرانی"
        ],
        "rating": 3.9,
        "votes": 243,
        "duration": "14 ساعت و 51 دقیقه",
        "size": "2 گیگابایت, 47.7 مگابایت",
        "transferable": false,
        "price": "144,900 تومان",
        "originalPrice": "207,000 تومان",
        "discount": "30%"
    },
    {
        "id": 24,
        "image": "https://img.taaghche.com/frontCover/87012.jpg?w=150",
        "title": "چگونه کمال‌گرا نباشیم؟",
        "author": "استفان گایز",
        "translator": "نرگس محمدی",
        "publisher": "انتشارات شمشاد",
        "categories": [
            "موفقیت و خودیاری",
            "روانشناسی",
        ],
        "rating": 3.9,
        "votes": 1444,
        "size": "490.3 کیلوبایت",
        "year": 1396,
        "pages": 244,
        "price": "50,000 تومان"
    },
    {
        "id": 25,
        "image": "https://img.taaghche.com/frontCover/74812.jpg?w=150",
        "title": "انسان در جست‌وجوی معنا",
        "author": "ویکتور فرانکل",
        "translator": "علیرضا ارجاع",
        "publisher": "انتشارات شمشاد",
        "categories": [
            "زندگی‌نامه",
            "روانشناسی",
            "مجموعه کتاب باز"
        ],
        "rating": 4.0,
        "votes": 1067,
        "size": "140.7 کیلوبایت",
        "year": 1399,
        "pages": 155,
        "price": "40,000 تومان",
        "discountedPrice": "20,000 تومان",
        "discountPercent": 50
    },
    {
        "id": 26,
        "image": "https://img.taaghche.com/frontCover/12208.jpg?w=150",
        "title": "باشگاه مغز(۱)",
        "author": "حامد اختیاری، تارا رضاپور",
        "publisher": "انتشارات مهرسا",
        "categories": [
            "سلامت",
            "موفقیت و خودیاری",
            "روانشناسی",
            "مجموعه باشگاه مغز"
        ],
        "rating": 3.8,
        "votes": 1064,
        "size": "12.8 مگابایت",
        "year": 1392,
        "pages": 284
    },
    {
        "id": 27,
        "image": "https://img.taaghche.com/frontCover/86144.jpg?w=150",
        "title": "مدیریت خشم دامیز",
        "author": "دویل جنتری",
        "translator": "عزیزالله سمیعی، سعید گرامی",
        "publisher": "انتشارات آوند دانش",
        "categories": [
            "موفقیت و خودیاری",
            "روانشناسی",
            "مجموعه کتاب‌های دامیز"
        ],
        "rating": 3.9,
        "votes": 151,
        "size": "3.6 مگابایت",
        "year": 1390,
        "pages": 363,
        "price": "88,000 تومان"
    },
    {
        "id": 28,
        "image": "https://img.taaghche.com/frontCover/30749.jpg?w=150",
        "title": "زندگی خود را دوباره بیافرینید",
        "author": "جفری ای.یانگ، جانت اس.کلوسکو",
        "translator": "ساره حسینی عطار",
        "publisher": "انتشارات شمشاد",
        "categories": [
            "موفقیت و خودیاری",
            "روانشناسی",
            "مجموعه کتاب باز",
            "مجموعه پرفروش‌های پاییز ۹۷",
            "پادکست شنبه این هفته"
        ],
        "rating": 3.7,
        "votes": 617,
        "size": "3.8 مگابایت",
        "year": 1396,
        "pages": 529,
        "price": "80,000 تومان"
    },
    {
        "id": 29,
        "image": "https://img.taaghche.com/frontCover/93663.jpg?w=150",
        "title": "بخت و اقبالی که در زندگی می یابیم",
        "author": "آلن دوباتن",
        "translator": "حمید هاشمی",
        "publisher": "نشر دنگ",
        "categories": [
            "موفقیت و خودیاری",
            "روانشناسی",
            "مجموعه مدرسه زندگی"
        ],
        "rating": 3.9,
        "votes": 168,
        "size": "2.6 مگابایت",
        "year": 1399,
        "pages": 189,
        "price": "11,000 تومان"
    },
    {
        "id": 30,
        "image": "https://img.taaghche.com/frontCover/82349.jpg?w=150",
        "title": "خودشناسی",
        "author": "آلن دوباتن",
        "translator": "محمدهادی حاجی‌بیگلو",
        "publisher": "انتشارات پندار تابان",
        "categories": [
            "موفقیت و خودیاری",
            "روانشناسی",
            "مجموعه مدرسه زندگی"
        ],
        "rating": 3.6,
        "votes": 1208
    },
    {
        "id": 31,
        "image": "https://img.taaghche.com/frontCover/59595.jpg?w=150",
        "title": "هنر نه گفتن",
        "author": "دیمون زاهاریادس",
        "translator": "میترا میرشکار",
        "publisher": "نشر علم",
        "categories": [
            "روانشناسی",
        ],
        "rating": 3.8,
        "votes": 391,
        "size": "110.4 کیلوبایت",
        "year": 1398,
        "pages": 159,
        "price": "45,000 تومان",
        "discountedPrice": "22,500 تومان",
        "discountPercent": 50
    },
    {
        "id": 32,
        "image": "https://img.taaghche.com/frontCover/118497.jpg?w=150",
        "title": "قدرت شروع ناقص",
        "author": "جیمز کلیر",
        "translator": "سیدایوب کوکبی، فرزانه حاج‌خلیلی",
        "publisher": "انتشارات میلکان",
        "categories": [
            "موفقیت و خودیاری",
            "روانشناسی",
        ],
        "rating": 3.5,
        "votes": 419,
        "size": "490.3 کیلوبایت",
        "year": 1400,
        "pages": 224,
        "price": "65,000 تومان",
        "discountedPrice": "32,500 تومان",
        "discountPercent": 50
    },
    {
        "id": 33,
        "image": "https://img.taaghche.com/frontCover/74810.jpg?w=150",
        "title": "شاید نیاز داری با یک نفر حرف بزنی",
        "author": "لوری گوتلیب",
        "translator": "بهاره پژومند",
        "publisher": "انتشارات شمشاد",
        "categories": [
            "روانشناسی", ,
            "مجموعه روان‌درمانی"
        ],
        "rating": 4.0,
        "votes": 279,
        "size": "658.1 کیلوبایت",
        "year": 1399,
        "pages": 454,
        "price": "42,000 تومان"
    },
    {
        "id": 34,
        "image": "https://img.taaghche.com/frontCover/228765.jpg?w=150",
        "title": "نامه هایی برای وقت هایی که حالت خوش نیست",
        "author": "جولی اسمیت",
        "translator": "آرزو شنطیائی",
        "publisher": "انتشارات میلکان",
        "categories": [
            "موفقیت و خودیاری",
            "روانشناسی",
        ],
        "rating": 3.6,
        "votes": 178,
        "size": "328.7 کیلوبایت",
        "year": 1403,
        "pages": 360,
        "price": "99,000 تومان",
        "discountedPrice": "49,500 تومان",
        "discountPercent": 50
    },
    {
        "id": 35,
        "image": "https://img.taaghche.com/frontCover/70533.jpg?w=150",
        "title": "وقتی نیچه گریست",
        "author": "اروین د.یالوم",
        "translator": "ساناز علمی",
        "publisher": "نشر علم",
        "categories": [
            "رمان",
            "داستان تاریخی",
            "داستان خارجی",
            "روانشناسی",
            "باشگاه کتاب‌خوانی ازتا"
        ],
        "rating": 4.0,
        "votes": 933,
        "size": "348.4 کیلوبایت",
        "year": 1398,
        "pages": 481,
        "price": "50,000 تومان",
        "discountedPrice": "25,000 تومان",
        "discountPercent": 50
    },
    {
        "id": 36,
        "image": "https://img.taaghche.com/frontCover/61770.jpg?w=150",
        "title": "بیمار خاموش",
        "author": "الکس مایکلیدیس",
        "translator": "مریم حسین‌نژاد",
        "publisher": "نشر سنگ",
        "categories": [
            "رمان",
            "داستان خارجی",
            "جنایی",
            "باشگاه کتاب‌خوانی ازتا",
            "پرفروش‌ترین‌های نیویورک تایمز"
        ],
        "rating": 4.4,
        "votes": 1777,
        "size": "265.6 کیلوبایت",
        "year": 1398,
        "pages": 320,
        "price": "80,000 تومان",
        "discountedPrice": "40,000 تومان",
        "discountPercent": 50
    },
    {
        "id": 37,
        "image": "https://img.taaghche.com/frontCover/196265.jpg?w=150",
        "title": "بخش دی",
        "author": "فریدا مک فادن",
        "translator": "صبا ایمانی",
        "publisher": "نشر نون",
        "categories": [
            "رمان",
            "داستان خارجی",
            "وحشت",
            "جنایی",
            "مجموعه منظومه داستان ترجمه"
        ],
        "rating": 4.3,
        "votes": 1177,
        "size": "266.6 کیلوبایت",
        "year": 1402,
        "pages": 264,
        "price": "49,000 تومان",
        "discountedPrice": "14,700 تومان",
        "discountPercent": 70
    },
    {
        "id": 38,
        "image": "https://img.taaghche.com/audioCover/187855.jpg?w=150",
        "title": "خاطرات یک آدم کش",
        "author": "کیم یونگ ها",
        "translator": "خاطره‌ کردکریمی",
        "narrator": "هوتن شکیبا",
        "publisher": "رادیو گوشه",
        "categories": [
            "رمان",
            "داستان خارجی",
            "وحشت",
            "جنایی"
        ],
        "rating": 4.0,
        "votes": 1152,
        "duration": "3 ساعت و 20 دقیقه",
        "size": "458.6 مگابایت",
        "transferable": false,
        "price": "100,000 تومان",
        "discountedPrice": "60,000 تومان",
        "discountPercent": 40
    },
    {
        "id": 39,
        "image": "https://img.taaghche.com/frontCover/104173.jpg?w=150",
        "title": "هشت قتل حرفه ای",
        "author": "پیتر سوانسون",
        "translator": "فرانک سالاری",
        "publisher": "نشر البرز",
        "categories": [
            "رمان",
            "داستان خارجی",
            "جنایی"
        ],
        "rating": 3.8,
        "votes": 514,
        "size": "217.9 کیلوبایت",
        "year": 1399,
        "pages": 256,
        "price": "32,000 تومان"
    },
    {
        "id": 40,
        "image": "https://img.taaghche.com/frontCover/3917.jpg?w=150",
        "title": "دختری در قطار",
        "author": "پائولا هاوکینز",
        "translator": "محبوبه موسوی",
        "publisher": "انتشارات میلکان",
        "categories": [
            "رمان",
            "داستان خارجی",
            "جنایی",
            "پرفروش‌ترین‌های ۹۴",
            "مجموعه اقتباس سینمایی"
        ],
        "rating": 3.8,
        "votes": 429,
        "size": "322.6 کیلوبایت",
        "year": 1394,
        "pages": 320,
        "price": "75,000 تومان",
        "discountedPrice": "37,500 تومان",
        "discountPercent": 50
    },
    {
        "id": 41,
        "image": "https://img.taaghche.com/frontCover/62958.jpg?w=150",
        "title": "کسب وکار خودتو راه بنداز",
        "author": "اندی پرینگل",
        "translator": "هانیه عظیم‌زادگان و همکاران",
        "publisher": "انتشارات پشتیبان",
        "categories": [
            "کسب و کار"
        ],
        "rating": 3.3,
        "votes": 35,
        "size": "2.6 مگابایت",
        "year": 1398,
        "pages": 260,
        "price": "30,000 تومان"
    },
    {
        "id": 42,
        "image": "https://img.taaghche.com/frontCover/52194.jpg?w=150",
        "title": "کسب و کار بلاکچین",
        "author": "ویلیام موگار",
        "translator": "پیمان رحمانی",
        "publisher": "راه پرداخت",
        "categories": [
            "کسب و کار"
        ],
        "rating": 3.0,
        "votes": 10,
        "size": "1.1 مگابایت",
        "year": 1397,
        "pages": 218,
        "price": "77,000 تومان"
    },
    {
        "id": 43,
        "image": "https://img.taaghche.com/frontCover/197045.jpg?w=150",
        "title": "کسب و کار پلتفرم ها",
        "author": "مایکل‌ کوسومانو و همکاران",
        "translator": "شهرام خلیل نژاد و همکاران",
        "publisher": "انتشارات آریانا قلم",
        "categories": [
            "کسب و کار"
        ],
        "rating": 3.4,
        "votes": 16,
        "size": "591.7 کیلوبایت",
        "year": 1402,
        "pages": 344,
        "price": "143,000 تومان"
    },
    {
        "id": 44,
        "image": "https://img.taaghche.com/frontCover/51439.jpg?w=150",
        "title": "کسب و کار ضد گلوله",
        "author": "اندرو گریفیتس",
        "translator": "خلیل رضوی، آزیتا طباطبایی رضوی",
        "publisher": "انتشارات کهکشان دانش",
        "categories": [
            "کسب و کار"
        ],
        "rating": 4.3,
        "votes": 15,
        "size": "126.7 کیلوبایت",
        "year": 1397,
        "pages": 146,
        "price": "59,000 تومان"
    },
    {
        "id": 45,
        "image": "https://img.taaghche.com/frontCover/105036.jpg?w=150",
        "title": "کسب و‌ کار خرده فروشی",
        "author": "ریک سگل",
        "translator": "سارا یوسفی، سیدمحمد کاظم عزیزی",
        "publisher": "انتشارات آوند دانش",
        "categories": [
            "کسب و کار"
        ],
        "rating": 2.9,
        "votes": 13,
        "size": "14.6 مگابایت",
        "year": 1398,
        "pages": 400,
        "price": "96,000 تومان"
    },
    {
        "id": 46,
        "image": "https://img.taaghche.com/frontCover/55149.jpg?w=150",
        "title": "یادگیری معکوس در دوره ابتدایی",
        "author": "جاناتان برگمن، آرون سمز",
        "translator": "سیدحسین رضوی خوسفی",
        "publisher": "انتشارات تیک",
        "categories": [
            "آموزشی",
            "آموزش",

        ],
        "rating": 4.0,
        "votes": 75,
        "size": "2.3 مگابایت",
        "year": 1396,
        "pages": 112,
        "price": "30,000 تومان"
    },
    {
        "id": 47,
        "image": "https://img.taaghche.com/frontCover/136294.jpg?w=150",
        "title": "آدمی همان است که می خواند",
        "author": "رابرت دی‌یانی",
        "translator": "علی‌ظفر قهرمانی‌نژاد",
        "publisher": "نشر بیدگل",
        "categories": [
            "آموزشی",
        ],
        "rating": 3.2,
        "votes": 63,
        "size": "339.8 کیلوبایت",
        "year": 1401,
        "pages": 299,
        "price": "165,000 تومان",
        "discountedPrice": "115,500 تومان",
        "discountPercent": 30
    },
    {
        "id": 48,
        "image": "https://img.taaghche.com/frontCover/126614.jpg?w=150",
        "title": "یادگیری یادگیری",
        "author": "باربارا اوکلی و همکاران",
        "translator": "حمید زعیمی",
        "publisher": "نشر نوین",
        "categories": [

            " آموزشی",
        ],
        "rating": 3.6,
        "votes": 111,
        "size": "5.2 مگابایت",
        "year": 1400,
        "pages": 248,
        "price": "80,000 تومان"
    },
    {
        "id": 49,
        "image": "https://img.taaghche.com/frontCover/63025.jpg?w=150",
        "title": "بازی‌های محلی ایران برای کودکان و نوجوانان",
        "author": "فاطمه بدرطالعی",
        "publisher": "کانون پرورش فکری کودکان و نوجوانان",
        "categories": [
            "آموزشی",
            "روان‌شناسی کودک و نوجوان",
            "کمپین دنیای قشنگ تو - ۹۸",
            "کمپین دنیای قشنگ تو - ۹۹"
        ],
        "rating": 4.1,
        "votes": 49,
        "size": "5.2 مگابایت",
        "year": 1396,
        "pages": 76,
        "price": "18,000 تومان",
        "discountedPrice": "9,000 تومان",
        "discountPercent": 50
    },
    {
        "id": 50,
        "image": "https://img.taaghche.com/frontCover/104654.jpg?w=150",
        "title": "اردو، ماجرای من است (دفتر اول)",
        "author": "حامد تقدیری",
        "publisher": "انتشارات فانوس دریا",
        "categories": [
            "آموزشی",

        ],
        "rating": 3.8,
        "votes": 49,
        "size": "1.2 مگابایت",
        "year": 1397,
        "pages": 58,
        "price": "15,000 تومان"
    },
    {
        "id": 51,
        "image": "https://img.taaghche.com/frontCover/86428.jpg?w=150",
        "title": "۱۰ جهش طلایی تا رتبه ۱۰۰",
        "author": "مهدی براتی",
        "publisher": "نشر ترآوا",
        "categories": [
            "آموزشی",
            " آموزشی",
            "موفقیت و خودیاری"
        ],
        "rating": 4.2,
        "votes": 151,
        "size": "1.6 مگابایت",
        "year": 1399,
        "pages": 196,
        "price": "14,750 تومان"
    },
];




// -----------------------------
// Search
// -----------------------------
document.addEventListener("DOMContentLoaded", () => {
    const searchInput = document.getElementById("search");
    if (searchInput) {
        searchInput.addEventListener("keydown", function (event) {
            if (event.key === "Enter") {
                const query = searchInput.value.trim();
                if (query !== "") {
                    window.location.href = `search.html?q=${encodeURIComponent(query)}`;
                }
            }
        });
    }
});

// -----------------------------
// Helper: تولید ستاره‌ها
// -----------------------------
function getStarHTML(rating) {
    const fullStars = Math.floor(rating);
    const hasHalfStar = rating % 1 >= 0.5;
    const emptyStars = 5 - fullStars - (hasHalfStar ? 1 : 0);

    let starsHTML = '';
    starsHTML += '<i class="fas fa-star"></i>'.repeat(fullStars);
    if (hasHalfStar) starsHTML += '<i class="fas fa-star-half-alt"></i>';
    starsHTML += '<i class="far fa-star"></i>'.repeat(emptyStars);

    return starsHTML;
}

// -----------------------------
// Slider کتاب‌های کودکان
// -----------------------------
function initChildrenSlider() {
    const slider = document.getElementById("slider");
    const prevBtn = document.getElementById("prevBtn");
    const nextBtn = document.getElementById("nextBtn");
    if (!slider || !prevBtn || !nextBtn) return;

    let currentIndex = 0;
    let visibleCards = 6;

    function updateVisibleCards() {
        const width = window.innerWidth;
        if (width <= 480) visibleCards = 1;
        else if (width <= 768) visibleCards = 2;
        else visibleCards = 6;
    }

    function getChildrenBooks() {
        return books.filter(book =>
            book.categories && book.categories.some(cat => cat.includes("کودک"))
        );
    }

    function renderChildrenBooks() {
        slider.innerHTML = "";
        const childrenBooks = getChildrenBooks();
        if (childrenBooks.length === 0) return;

        childrenBooks.forEach(book => {
            const card = document.createElement("div");
            card.classList.add("book-card");
            card.innerHTML = `
                <img src="${book.image}" alt="${book.title}" />
                <div class="stars">${getStarHTML(book.rating)}</div>
                <h3 class="book-title">${book.title}</h3>
                <p class="book-author">${book.author}</p>
                <p class="book-price">${book.price}</p>
            `;
            card.addEventListener("click", () => {
                window.location.href = `details.html?id=${book.id}`;
            });
            slider.appendChild(card);
        });

        updateButtons();
        updateSliderPosition();
    }

    function updateButtons() {
        const childrenBooks = getChildrenBooks();
        prevBtn.disabled = currentIndex === 0;
        nextBtn.disabled = currentIndex >= childrenBooks.length - visibleCards;
    }
function updateSliderPosition() {
    const card = slider.querySelector(".book-card");
    if (!card) return;
    const cardWidth = card.offsetWidth + 20;

    // پروژه RTL → برعکس
    slider.style.transform = `translateX(${currentIndex * cardWidth}px)`;
}
    function onResize() {
        updateVisibleCards();
        const childrenBooks = getChildrenBooks();
        if (currentIndex > childrenBooks.length - visibleCards) {
            currentIndex = Math.max(0, childrenBooks.length - visibleCards);
        }
        updateButtons();
        updateSliderPosition();
    }

    prevBtn.addEventListener("click", () => {
        if (currentIndex > 0) {
            currentIndex--;
            updateButtons();
            updateSliderPosition();
        }
    });

    nextBtn.addEventListener("click", () => {
        const childrenBooks = getChildrenBooks();
        if (currentIndex < childrenBooks.length - visibleCards) {
            currentIndex++;
            updateButtons();
            updateSliderPosition();
        }
    });

    window.addEventListener("resize", onResize);

    updateVisibleCards();
    renderChildrenBooks();
}

// -----------------------------
// کتاب‌های صوتی (شبکه ثابت)
// -----------------------------
function renderHotBooks() {
    const container = document.getElementById("hotBooksGrid");
    if (!container) return;

    const hotBooks = books.filter(book =>
        (book.categories && book.categories.some(cat =>
            cat.includes("صوتی") || cat.includes("کتاب صوتی")
        )) ||
        (book.title && book.title.includes("کتاب صوتی"))
    );

    if (hotBooks.length === 0) return;

    // فقط 12 کتاب اول
    hotBooks.slice(0, 12).forEach(book => {
        const bookCard = document.createElement("div");
        bookCard.className = "book-card";
        bookCard.innerHTML = `
            <img src="${book.image}" alt="${book.title}" loading="lazy">
            <div class="book-info">
                <div class="stars">${getStarHTML(book.rating)} (${book.rating.toFixed(1)})</div>
                <h3 class="book-title">${book.title}</h3>
                <p class="book-author">نویسنده: ${book.author}</p>
                <div class="price-container">
                    <div>
                        <span class="original-price">${book.originalPrice || ""}</span>
                        <span class="current-price">${book.price || ""}</span>
                    </div>
                    <span class="discount-badge">${book.discount || ""}</span>
                </div>
            </div>
        `;
        bookCard.addEventListener("click", () => {
            window.location.href = `details.html?id=${book.id}`;
        });
        container.appendChild(bookCard);
    });
}

// -----------------------------
// Menu, Cart, Overlay
// -----------------------------
function initMenuCart() {
    const hamburger = document.getElementById("hamburger");
    const mobileNav = document.getElementById("mobileNav");
    const closeMenu = document.getElementById("closeMenu");
    const overlay = document.getElementById("overlay");
    const cartIcon = document.getElementById("cartIcon");
    const mobileCartBtn = document.getElementById("mobileCartBtn");
    const cartSidebar = document.getElementById("cartSidebar");
    const closeCart = document.getElementById("closeCart");

    if (hamburger && mobileNav && closeMenu && overlay) {
        hamburger.addEventListener("click", () => {
            mobileNav.classList.add("active");
            overlay.classList.add("active");
        });
        closeMenu.addEventListener("click", () => {
            mobileNav.classList.remove("active");
            overlay.classList.remove("active");
        });
        overlay.addEventListener("click", () => {
            mobileNav.classList.remove("active");
            cartSidebar?.classList.remove("active");
            overlay.classList.remove("active");
        });
    }

    if (cartIcon && cartSidebar && overlay) {
        cartIcon.addEventListener("click", () => {
            cartSidebar.classList.add("active");
            overlay.classList.add("active");
        });
    }
    if (mobileCartBtn && cartSidebar && overlay) {
        mobileCartBtn.addEventListener("click", () => {
            cartSidebar.classList.add("active");
            overlay.classList.add("active");
        });
    }
    if (closeCart && cartSidebar && overlay) {
        closeCart.addEventListener("click", () => {
            cartSidebar.classList.remove("active");
            overlay.classList.remove("active");
        });
    }
}

// -----------------------------
// Initialize everything
// -----------------------------

document.addEventListener("DOMContentLoaded", () => {
    initChildrenSlider();
    renderHotBooks();
    initMenuCart();
});
