document.addEventListener('DOMContentLoaded', function () {
   const quantityInputs = document.querySelectorAll('.quantity-input');
   const increaseButtons = document.querySelectorAll('.increase-qty');
   const decreaseButtons = document.querySelectorAll('.decrease-qty');

   function updateQuantity(productId, newQuantity) {
      fetch(`/Cart/UpdateQuantity?productId=${productId}&quantity=${newQuantity}`, {
         method: 'POST'
      }).then(response => {
         if (response.ok) {
            location.reload(); // Refresh the page to reflect new quantities and total price
         }
      }).catch(error => {
         console.error('Error:', error);
      });
   }

   // Handle direct input change
   quantityInputs.forEach(input => {
      input.addEventListener('change', function () {
         const productId = input.getAttribute('data-productid');
         const newQuantity = input.value;
         updateQuantity(productId, newQuantity);
      });
   });

   // Increase Button
   increaseButtons.forEach(button => {
      button.addEventListener('click', function () {
         const productId = button.getAttribute('data-productid');
         const inputField = document.querySelector(`.quantity-input[data-productid="${productId}"]`);
         inputField.value = parseInt(inputField.value) + 1;
         updateQuantity(productId, inputField.value);
      });
   });

   // Decrease Button
   decreaseButtons.forEach(button => {
      button.addEventListener('click', function () {
         const productId = button.getAttribute('data-productid');
         const inputField = document.querySelector(`.quantity-input[data-productid="${productId}"]`);
         if (parseInt(inputField.value) > 1) {
            inputField.value = parseInt(inputField.value) - 1;
            updateQuantity(productId, inputField.value);
         }
      });
   });
});



document.querySelectorAll('.btn-remove').forEach(button => {
   button.addEventListener('click', function () {
      const productId = this.getAttribute('data-productid');

      fetch('/Cart/RemoveItem', {
         method: 'POST',
         headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
         body: `productId=${productId}`
      }).then(response => {
         if (response.ok) {
            location.reload();
         } else {
            alert('Error removing item.');
         }
      });
   });
});