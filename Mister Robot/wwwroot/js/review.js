document.addEventListener("DOMContentLoaded", function () {
   const reviewForm = document.querySelector("form");
   const reviewTextArea = document.querySelector(".text-review");
   const ratingValue = document.getElementById("ratingValue");
   const stars = document.querySelectorAll(".rating .star");

   reviewForm.addEventListener("submit", function (event) {
      if (ratingValue.value === "0") {
         event.preventDefault();
         alert("Please select a star rating before submitting your review.");
      }
   });

  
   stars.forEach(star => {
      star.addEventListener("click", function () {
         const value = parseInt(this.getAttribute("data-value"));
         ratingValue.value = value;

         // Clear all stars and fill based on selection
         stars.forEach(s => s.classList.remove('bxs-star'));
         stars.forEach(s => s.classList.add('bx-star'));

         for (let i = 0; i < value; i++) {
            stars[i].classList.remove('bx-star');
            stars[i].classList.add('bxs-star');
         }
      });
   });
});
