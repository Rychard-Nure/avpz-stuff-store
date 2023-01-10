var div = document.getElementById('datadiv');

function clearData() {
    cartItems = [];
    localStorage.setItem('cartItems', JSON.stringify(cartItems));
}

function getCartItemsCount() {
    let b = document.getElementById('itemsCount');
    const items = JSON.parse(localStorage.getItem('cartItems'));
    if (items != null) {
        b.innerText = items.length;
    }
    else {
        b.innerText = "0";
    }
    getTotalPrice();
}


// set old items list to document 
const items = JSON.parse(localStorage.getItem('cartItems'));
let cartItems = Array();
if (items) {
    cartItems = [...items];
}

function addItemToStorage(item) {
    if (cartItems.some(i => i.id == item.id)) {
        this.cart = cartItems.find(i => i.id == item.id);
        this.cart.count++;
    }
    else {
        item.count = 1;
        cartItems.push(item);
    }
    localStorage.setItem('cartItems', JSON.stringify(cartItems));
    getCartItemsCount();
}

function addbtnClick() {
    let name = document.getElementById('name').innerText;
    let price = document.getElementById('price').innerText;
    let image = document.getElementById('detail-img').src;
    let id = document.getElementById('gameId').value;
    const item = {
        id: id,
        name: name,
        price: price,
        image: image,
        count: 0
    }

    addItemToStorage(item);
}


function fillData() {
    const items = JSON.parse(localStorage.getItem('cartItems'));
    if (items != null) {
        div.innerHTML = "";
        cartItems.forEach(item => div.append(cartItem(item)));


        var totalPrice = 0;
        var prices = div.getElementsByClassName('price');
        var counters = div.getElementsByClassName('count');
        var total = document.getElementById('total');
        var ids = div.getElementsByClassName('itemId');
        var counts = [];
        for (let i = 0; i < counters.length; i++) {
            counts[i] = counters[i].value;
        }

        function getTotalPrice() {
            totalPrice = 0;
            for (let i = 0; i < counters.length; i++) {
                totalPrice += parseInt(prices[i].innerText) * parseInt(counts[i]);
            }

            total.innerHTML = "$ " + totalPrice;
        }

        div.querySelectorAll('.btn_minus').forEach(btn => btn.addEventListener('click', event => {
            var index1 = Array.from(div.querySelectorAll('.btn_minus')).indexOf(btn);
            if (counts[index1] > 1) {
                counts[index1]--;
                counters[index1].value = counts[index1];
            }
            getTotalPrice();
        }));

        div.querySelectorAll('.btn_plus').forEach(btn => btn.addEventListener('click', event => {
            var index1 = Array.from(div.querySelectorAll('.btn_plus')).indexOf(btn);
            counts[index1]++;
            counters[index1].value = counts[index1];
            getTotalPrice();
        }));

        div.querySelectorAll('.remove').forEach(btn => btn.addEventListener('click', function () {
            var index = Array.from(div.querySelectorAll('.remove')).indexOf(btn);
            var itemId = ids[index].value;
            cartItems = cartItems.filter(i => {
                if (i.id != itemId) return i;
            });
            localStorage.setItem('cartItems', JSON.stringify(cartItems));
            fillData();
            getTotalPrice();
            getCartItemsCount();
        }));

        getTotalPrice();
    }
}

function cartItem(data) {
    var item = `
        <div class="row my-4 px-2">
        <div class="col-md-3">
            <input type="hidden" class="itemId" value="${data.id}" />
            <img src="${data.image}" class="cart-item-img" alt="">
        </div>
        <div class="col-md-4">
            <h5>${data.name}</h5>
            <h6>$ <span class="price">${data.price}</span></h6>
        </div>
        <div class="col-md-5">
            <div class="d-flex justify-content-between counter">
                <button class="btn btn_minus"><i class="las la-minus"></i></button>
                <input disabled readonly type="text" class="count" value="${data.count}"/>
                <button class="btn btn_plus"><i class="las la-plus"></i></button>
            </div>
            
            <a class="remove btn text-danger float-end"><i class="las la-trash"></i></a>
        </div>
        </div>
        <hr>
    `;

    var div = document.createElement("div");
    div.innerHTML = item;
    return div;
}