// تعريف order متاح لكل السكريبت
var order = JSON.parse(sessionStorage.getItem("currentOrder")) || [];

window.addEventListener("load", function () {
    var path = window.location.pathname;

    // ====================
    // كود صفحة اختيار الأطباق
    // ====================
    if (path.includes("/Order/SelectDishes")) {
        var orderJson = sessionStorage.getItem("currentOrder");
        var input = document.getElementById("orderDetailsInput");
        if (orderJson && input) {
            input.value = orderJson;
        }
        updateOrderSummary();
    }

    // ====================
    // كود صفحة تأكيد الطلب
    // ====================
    else if (path.includes("/Order/CreateOrder")) {
        var orderJson = sessionStorage.getItem("currentOrder");
        if (orderJson) {
            const orderDetails = JSON.parse(orderJson);
            document.getElementById("orderDetailsInput").value = orderJson;
            displayOrderDetails(orderDetails);
        } else {
            window.location.href = "/Order/SelectDishes";
        }
    }
});

// ====================
// دوال اختيار الأطباق
// ====================

function increaseQuantity(id) {
    var qtySpan = document.getElementById('qty-' + id);
    var currentQty = parseInt(qtySpan.innerText);
    currentQty++;
    qtySpan.innerText = currentQty;
}

function decreaseQuantity(id) {
    var qtySpan = document.getElementById('qty-' + id);
    var currentQty = parseInt(qtySpan.innerText);
    if (currentQty > 0) {
        currentQty--;
        qtySpan.innerText = currentQty;
    }
}

function addToOrder(id, name, price) {
    var qtySpan = document.getElementById('qty-' + id);
    var quantity = parseInt(qtySpan.innerText);

    if (quantity > 0) {
        const dishId = parseInt(id);
        const existing = order.find(d => d.DishId === dishId);

        if (existing) {
            existing.Quantity += quantity;
        } else {
            order.push({
                DishId: dishId,
                Name: name,
                Price: parseFloat(price),
                Quantity: quantity
            });
        }

        sessionStorage.setItem("currentOrder", JSON.stringify(order));
        updateOrderSummary();
        qtySpan.innerText = 0;
        document.getElementById('bottomNavbar').classList.remove('d-none');
    }
}

function calculateTotalPrice() {
    return order.reduce((total, item) => total + (item.Price * item.Quantity), 0).toFixed(2);
}

function updateOrderSummary() {
    var orderSummary = document.getElementById('orderSummary');
    if (!orderSummary) return;

    orderSummary.innerHTML = '';

    if (order.length === 0) {
        document.getElementById('bottomNavbar').classList.add('d-none');
        return;
    }

    var totalDiv = document.createElement('div');
    totalDiv.className = "me-3 text-success";
    totalDiv.innerHTML = `Total: ${calculateTotalPrice()} EGP`;
    orderSummary.appendChild(totalDiv);

    order.forEach(item => {
        var itemDiv = document.createElement('div');
        itemDiv.className = "me-2";
        itemDiv.innerHTML = `
            ${item.Name}
            <span class="badge bg-success">${item.Quantity}</span>
            <button class="btn btn-sm btn-outline-danger ms-1" onclick="removeItem(${item.DishId})">
                <i class="bi bi-x-circle"></i>
            </button>
        `;
        orderSummary.appendChild(itemDiv);
    });
}


function removeItem(id) {
    order = order.filter(d => d.DishId != id);
    sessionStorage.setItem("currentOrder", JSON.stringify(order));
    updateOrderSummary();
}

function submitOrder() {
    if (order.length > 0) {
        sessionStorage.setItem("currentOrder", JSON.stringify(order));

        var form = document.createElement("form");
        form.method = "POST";
        form.action = "/Order/CreateOrder";

        var input = document.createElement("input");
        input.type = "hidden";
        input.name = "orderDetailsJson";
        input.value = JSON.stringify(order);
        form.appendChild(input);

        document.body.appendChild(form);
        form.submit();
    } else {
        alert("Please add at least one dish to your order.");
    }
}

// ====================
// دوال صفحة تأكيد الطلب
// ====================

function displayOrderDetails(orderDetails) {
    const tableBody = document.getElementById("orderDetailsTable");
    const totalPriceElement = document.getElementById("totalPrice");
    let totalPrice = 0;

    tableBody.innerHTML = "";

    orderDetails.forEach(item => {
        const subtotal = item.Price * item.Quantity;
        totalPrice += subtotal;

        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${item.DishId}</td>
            <td>${item.Name}</td>
            <td>${item.Quantity}</td>
            <td>${item.Price.toFixed(2)} EGP</td>
            <td>${subtotal.toFixed(2)} EGP</td>
        `;
        tableBody.appendChild(row);
    });

    const model = orderDetails.map(item => ({
        DishId: item.DishId,
        Price: item.Price,
        Quantity: item.Quantity
    }));

    for (let i = 0; i < model.length; i++) {
        const item = model[i];

        const dishIdInput = document.createElement("input");
        dishIdInput.type = "hidden";
        dishIdInput.name = `OrderDetails[${i}].DishId`;
        dishIdInput.value = item.DishId;
        document.getElementById("orderForm").appendChild(dishIdInput);

        const priceInput = document.createElement("input");
        priceInput.type = "hidden";
        priceInput.name = `OrderDetails[${i}].Price`;
        priceInput.value = item.Price;
        document.getElementById("orderForm").appendChild(priceInput);

        const quantityInput = document.createElement("input");
        quantityInput.type = "hidden";
        quantityInput.name = `OrderDetails[${i}].Quantity`;
        quantityInput.value = item.Quantity;
        document.getElementById("orderForm").appendChild(quantityInput);
    }

    totalPriceElement.textContent = `Total: ${totalPrice.toFixed(2)} EGP`;

    const orderPriceInput = document.createElement("input");
    orderPriceInput.type = "hidden";
    orderPriceInput.name = "orderprice";
    orderPriceInput.value = totalPrice.toFixed(2);
    document.getElementById("orderForm").appendChild(orderPriceInput);
}
