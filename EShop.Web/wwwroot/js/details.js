// notification toast
(function () {
    if (window.showToast) return;
    const queue = [];

    // function for adding tags dynamically
    const add = (tag, attrs, onload) => {
        const el = document.createElement(tag); // create new element
        Object.assign(el, attrs); // assign attributes to element
        if (onload) el.onload = onload;
        document.head.appendChild(el); // append to head
    };

    // function for showing toast
    window.showToast = msg => {
        const o = {
            text: msg,
            duration: 3000,
            close: true,
            gravity: "bottom",
            position: "left",
            style: { background: "linear-gradient(to right, #2ecc71, #27ae60)", color: "#fff" }
        };
        window.Toastify ? Toastify(o).showToast() : queue.push(o);
    };

    // add css and js for toastify
    add("link", { rel: "stylesheet", href: "https://cdn.jsdelivr.net/npm/toastify-js/src/toastify.min.css" });
    add("script", { src: "https://cdn.jsdelivr.net/npm/toastify-js" }, () => {
        queue.forEach(o => Toastify(o).showToast());
        queue.length = 0;
    });
})();

// get cart from localStorage
const getCart = () => JSON.parse(localStorage.getItem('cart') || "[]");

// set cart to localStorage
const setCart = c => localStorage.setItem('cart', JSON.stringify(c));

// create star rating
function getStars(rating) {
    const fullStars = Math.floor(rating); // full stars
    const halfStar = rating % 1 >= 0.5; // check if half star needed
    let starsHtml = "";

    for (let i = 0; i < fullStars; i++) {
        starsHtml += "★"; // add full star
    }
    if (halfStar) {
        starsHtml += "☆"; // add half star
    }
    while (starsHtml.length < 5) {
        starsHtml += "☆"; // fill empty with stars
    }
    return starsHtml;
}

// update cart count in header
function updateCartCount() {
    try {
        const total = getCart().reduce((s, it) => s + (it.quantity || 1), 0); // total items
        document.querySelectorAll(".cart-count").forEach(el => el.textContent = total);
    } catch (e) {
        console.warn("error updating count", e);
    }
}

// render cart items in sidebar
function renderCart() {
    const cart = getCart();
    const list = document.getElementById("cartItems"); // container for items
    const totalEl = document.getElementById('cartTotal'); // container for total
    updateCartCount();
    list.innerHTML = ""; // clear content

    if (!cart.length) {
        list.innerHTML = `<div class="empty-cart"><p>سبد خرید شما خالی است</p></div>`;
        totalEl.textContent = "0 تومان";
        return;
    }

    let total = 0; // total price
    cart.forEach((item, index) => {
        const price = parseInt(item.price.replace(/[^0-9]/g, "")) || 0; // clean price string
        const q = item.quantity || 1; // item quantity
        const itemTotal = price * q; // item total
        total += itemTotal; // add to total

        // render each cart item
        list.insertAdjacentHTML("beforeend", `
            <div class="cart-item">
                <div class="item-info">
                    <img src="${item.image}" alt="${item.title}" />
                    <div>
                        <p>${item.title}</p>
                        <p>${itemTotal.toLocaleString()} تومان</p>
                    </div>
                </div>
                <div class="item-actions">
                    <div class="quantity-control">
                        <button class="decrease-btn" data-index="${index}">-</button>
                        <span class="quantity">${q}</span>
                        <button class="increase-btn" data-index="${index}">+</button>
                    </div>
                    <button class="remove-btn" data-index="${index}">حذف</button>
                </div>
            </div>
        `);
    });

    // add events for cart buttons
    list.querySelectorAll(".decrease-btn").forEach(btn =>
        btn.onclick = () => decreaseQuantity(+btn.dataset.index)
    );
    list.querySelectorAll(".increase-btn").forEach(btn =>
        btn.onclick = () => increaseQuantity(+btn.dataset.index)
    );
    list.querySelectorAll(".remove-btn").forEach(btn =>
        btn.onclick = () => removeFromCart(+btn.dataset.index)
    );

    totalEl.textContent = total.toLocaleString() + " تومان"; // update total
}

// decrease quantity function
function decreaseQuantity(index) {
    const cart = getCart();
    if (!cart[index]) return;
    cart[index].quantity > 1 ? cart[index].quantity-- : cart.splice(index, 1);
    setCart(cart);
    renderCart();
}

// increase quantity function
function increaseQuantity(index) {
    const cart = getCart();
    if (!cart[index]) return;
    cart[index].quantity = (cart[index].quantity || 1) + 1;
    setCart(cart);
    renderCart();
}

// remove item from cart
function removeFromCart(index) {
    const cart = getCart();
    cart.splice(index, 1);
    setCart(cart);
    renderCart();
}

// wait for page to load
document.addEventListener('DOMContentLoaded', function () {
    // elements for menu
    const hamburger = document.getElementById('hamburger');
    const mobileNav = document.getElementById('mobileNav');
    const closeMenu = document.getElementById('closeMenu');
    const overlay = document.getElementById('overlay');

    // elements for cart sidebar
    const cartIcon = document.getElementById('cartIcon');
    const mobileCartBtn = document.getElementById('mobileCartBtn');
    const cartSidebar = document.getElementById('cartSidebar');
    const closeCart = document.getElementById('closeCart');

    // helper functions
    const show = (el) => el.classList.add('active'); // show element
    const hide = (el) => el.classList.remove('active'); // hide element

    // events for menu
    hamburger.onclick = () => { show(mobileNav); show(overlay); };
    closeMenu.onclick = () => { hide(mobileNav); hide(overlay); };
    overlay.onclick = () => { hide(mobileNav); hide(cartSidebar); hide(overlay); };

    // events for cart sidebar
    cartIcon.onclick = () => { renderCart(); show(cartSidebar); show(overlay); };
    mobileCartBtn.onclick = () => { hide(mobileNav); renderCart(); show(cartSidebar); show(overlay); };
    closeCart.onclick = () => { hide(cartSidebar); hide(overlay); };

    updateCartCount();
    renderBookDetails(); // render book details on load
});

// render book details page
function renderBookDetails() {
    const params = new URLSearchParams(window.location.search); // get URL params
    const bookId = parseInt(params.get("id")); // get book id
    const book = books.find(b => b.id === bookId); // find book from list

    const container = document.getElementById("book-details"); // book details container
    const relatedContainer = document.getElementById("related-books"); // related books container

    if (!book) { container.innerHTML = "<p>کتابی پیدا نشد</p>"; return; }

    // render book details HTML
    container.innerHTML = `
    <div class="details-card">
      <img src="${book.image}" alt="${book.title}" class="book-image" />
      <div class="info">
        <h2 class="title">${book.title}</h2>
        <p><strong>نویسنده:</strong> ${book.author}</p>
        ${book.translator ? `<p><strong>مترجم:</strong> ${book.translator}</p>` : ""}
        <p><strong>ناشر:</strong> ${book.publisher}</p>
        <p><strong>دسته‌بندی‌ها:</strong> ${book.categories.join("، ")}</p>
        <p><strong>امتیاز:</strong> <span class="stars">${getStars(book.rating)}</span> (${book.votes} رای)</p>
        <p class="price"><strong>قیمت:</strong> ${book.price} تومان</p>
        <p><button id="addToCartBtn" class="add-to-cart-btn">افزودن به سبد خرید</button></p>
      </div>
    </div>
  `;

    // add to cart button
    document.getElementById("addToCartBtn").onclick = function () {
        const cart = getCart();
        const i = cart.findIndex(it => it.id === book.id);
        if (i === -1) {
            cart.push({ ...book, quantity: 1 }); // add new item
            setCart(cart);
            updateCartCount();
            typeof showToast === "function"
                ? showToast(`${book.title} به سبد خرید اضافه شد.`)
                : alert("کتاب به سبد خرید اضافه شد.");
        } else {
            cart[i].quantity = (cart[i].quantity || 1) + 1; // increase quantity
            setCart(cart);
            updateCartCount();
            typeof showToast === "function"
                ? showToast(`تعداد "${cart[i].title}" افزایش یافت.`)
                : alert("تعداد کتاب افزایش یافت.");
        }
        renderCart(); // refresh cart
    };

    // render related books
    const related = books
        .filter(b => b.id !== book.id && b.categories.some(cat => book.categories.includes(cat)))
        .slice(0, 4);

    if (!related.length) { relatedContainer.innerHTML = "<p>کتاب مرتبطی پیدا نشد</p>"; return; }

    relatedContainer.innerHTML = "<h3 class='related-list-header'>کتاب‌های مرتبط</h3><div class='related-list'></div>";
    const list = relatedContainer.querySelector(".related-list");
    related.forEach(rb => {
        const card = document.createElement("div");
        card.className = "related-card";
        card.innerHTML = `<img src="${rb.image}" alt="${rb.title}" /><p class="related-title">${rb.title}</p>`;
        card.onclick = () => { window.location.href = `details.html?id=${rb.id}`; }; // go to related book page
        list.appendChild(card);
    });
}
