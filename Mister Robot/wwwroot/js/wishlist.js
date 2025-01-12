document.querySelectorAll(".wishlist-button").forEach(button => {
   button.addEventListener("click", function (event) {
      event.preventDefault();
      const form = this.closest('form');
      fetch(form.action, {
         method: 'POST',
         body: new FormData(form)
      }).then(response => {
         location.reload(); // Refresh the page to reflect the changes
      });
   });
});
