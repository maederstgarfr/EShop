



// دریافت کوئری از URL
const urlParams = new URLSearchParams(window.location.search);
const query = urlParams.get("q")?.trim();

document.getElementById("search-title").innerText = `نتایج برای: "${query}"`;

// نرمال‌سازی برای تطابق بهتر
function normalize(text) {
    return text.trim();
}

// فیلتر نتایج
const results = books.filter(book => {
    const title = normalize(book.title);
    const author = normalize(book.author);
    const publisher = normalize(book.publisher);
    const categoryList = book.categories.map(cat => normalize(cat)).join(" ");
    const searchText = normalize(query);

    return (
        title.includes(searchText) ||
        author.includes(searchText) ||
        publisher.includes(searchText) ||
        categoryList.includes(searchText)
    );
});

// نمایش نتایج
const resultsContainer = document.getElementById("search-results");

if (results.length === 0) {
    resultsContainer.innerHTML = "<p>کتابی یافت نشد.</p>";
} else {
    results.forEach(book => {
        const div = document.createElement("div");
        div.classList.add("book-card");
        
        // اضافه کردن لینک به جزئیات کتاب
        div.innerHTML = `
            <a href="details.html?id=${book.id}" >
                <img src="${book.image}" alt="${book.title}" />
                <h3>${book.title}</h3>
                <p>نویسنده: ${book.author}</p>
                <p>ناشر: ${book.publisher}</p>
                <p>قیمت: ${book.price}</p>
            </a>
        `;

        resultsContainer.appendChild(div);
    });
}

