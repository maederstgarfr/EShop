
//get by params
const urlParams = new URLSearchParams(window.location.search)
//get category by address
const categoryParam = urlParams.get('category');

function normilize(text) {
    return text?.trim()
}

const normilizedCategory = normilize(categoryParam)

//show category title

document.getElementById("category-title").innerText = `دسته بندی: ${categoryParam}`;


//fillter books by category
const filteredBooks = books.filter(book =>
    book.categories.some(cat => normilize(cat) === normilizedCategory)
)

const container = document.getElementById('books-container')

if (filteredBooks.length === 0) {
    container.innerHTML = "<p> کتابی با این دسته بندی پیدا نشد</p>"
} else {
    filteredBooks.forEach(book => {
        //create elemet and add class
        const bookDiv = document.createElement("div");
        bookDiv.className = "book";

        // add details book
        bookDiv.innerHTML = `
        <img src="${book.image}"/>
        <h3>${book.title}</h3>
        <p>نویسنده :${book.author}</p>
        <p>ناشر :${book.publisher}</p>
        <p>امتیاز:${book.rating} (${book.votes})</p>
        <p>قیمت :${book.price}</p>

`;

        bookDiv.style.cursor = "pointer"
        bookDiv.addEventListener("click", () => {
            window.location.href = `details.html?id=${book.id}`;
        })

        container.appendChild(bookDiv);

    })
}






