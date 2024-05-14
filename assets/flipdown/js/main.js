document.addEventListener('DOMContentLoaded', () => {

  // Unix timestamp (in seconds) to count down to
  var twoDaysFromNow = (new Date().getTime() / 1000) + (86400 * 2) + 1;

    var dateString = '2023/12/13';
    var timestamp = new Date(dateString).getTime() / 1000;
    console.log(timestamp);
    var flipdown = new FlipDown(timestamp)
    .start()
    .ifEnded(() => {
      console.log('The countdown has ended!');
    });
});
