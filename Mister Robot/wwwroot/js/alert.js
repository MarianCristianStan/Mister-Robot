window.onload = function () {
   const alert = document.querySelector('.alert');
   if (alert) {
      alert.classList.add('show');
      setTimeout(() => {
         alert.classList.remove('show');
      }, 4000);
   }
};
