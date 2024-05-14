<%@ Control Language="C#" AutoEventWireup="true" CodeFile="review.ascx.cs" Inherits="template_review" %>
<div id="reviews" class="reviews mb-4">
    <div class="reviews__heading">1 đánh giá cho <span>Loa tháp Samsung MX-ST50B/XV</span></div>

    <div class="reviews__box d-sm-flex align-items-center mb-4">
        <div class="reviews__box-left col">
            <div class="reviews__average">
                <div class="d-sm-flex align-items-center">
                    <div class="d-flex align-items-center">
                        <strong class="number me-2">5.00</strong>
                        <div class="rating mb-0 me-2">
                            <div class="rating__inner">
                                <span class="rating__pre" style="width: 100%"></span>
                            </div>
                        </div>
                    </div>
                    <a href="#reviews" class="txt-link" rel="nofollow"><span class="count">1</span> đánh giá của khách hàng</a>
                </div>
            </div>
            <div class="reviews__bar">
                <div class="reviews__bar-inner d-flex align-items-center">
                    <span class="reviews__value">5 <i class="icon-star"></i></span>
                    <span class="col reviews__bar-rating">
                        <span class="reviews__rating">
                            <span class="reviews__rating-pre" style="width: 100%;"></span>
                        </span>
                    </span>
                    <span class="reviews__num"><b>100%</b> | 1 đánh giá</span>
                </div>
                <div class="reviews__bar-inner d-flex align-items-center">
                    <span class="reviews__value">4 <i class="icon-star"></i></span>
                    <span class="col reviews__bar-rating">
                        <span class="reviews__rating">
                            <span class="reviews__rating-pre" style="width: 40%;"></span>
                        </span>
                    </span>
                    <span class="reviews__num"><b>0%</b> | 0 đánh giá</span>
                </div>
                <div class="reviews__bar-inner d-flex align-items-center">
                    <span class="reviews__value">3 <i class="icon-star"></i></span>
                    <span class="col reviews__bar-rating">
                        <span class="reviews__rating">
                            <span class="reviews__rating-pre" style="width: 20%;"></span>
                        </span>
                    </span>
                    <span class="reviews__num"><b>0%</b> | 0 đánh giá</span>
                </div>
                <div class="reviews__bar-inner d-flex align-items-center">
                    <span class="reviews__value">2 <i class="icon-star"></i></span>
                    <span class="col reviews__bar-rating">
                        <span class="reviews__rating">
                            <span class="reviews__rating-pre" style="width: 70%;"></span>
                        </span>
                    </span>
                    <span class="reviews__num"><b>0%</b> | 0 đánh giá</span>
                </div>
                <div class="reviews__bar-inner d-flex align-items-center">
                    <span class="reviews__value">1  <i class="icon-star"></i></span>
                    <span class="col reviews__bar-rating">
                        <span class="reviews__rating">
                            <span class="reviews__rating-pre" style="width: 64%;"></span>
                        </span>
                    </span>
                    <span class="reviews__num"><b>0%</b> | 0 đánh giá</span>
                </div>
            </div>
        </div>
        <div class="reviews__box-right">
            <button type="button" title="Đánh giá ngay" class="btn btn-primary btn-reviews-now" data-bs-toggle="modal" data-bs-target="#modaReview">Đánh giá ngay</button>
        </div>
    </div>

    <!-- Review list -->
    <ol class="reviews__list">
        <li class="reviews__list-item">
            <div class="reviews__list-inner">
                <div class="comment-text">
                    <h3 class="reviews__name">Hai Nguyen</h3>
                    <div class="reviews__description">
                        <div class="rating">
                            <div class="rating__inner">
                                <span class="rating__pre" style="width: 45%"></span>
                            </div>
                        </div>
                        <p>Loa nghe khá ngon trong tầm giá mua lúc khuyến mãi nghe khá hay mình khó tính vẫn thấy ưng</p>
                    </div>
                </div>
                <div class="reviews__action d-flex align-items-center">
                    <a rel="nofollow" class="comment-reply-link txt-link" href="#comment-15078" data-commentid="15078" data-postid="69376" data-belowelement="comment-15078" data-respondelement="respond" data-replyto="Phản hồi đến Hai Nguyen" aria-label="Phản hồi đến Hai Nguyen">Trả lời</a>
                    <span class="space"> • </span>
                    <a href="javascript:void(0)" class="cmtlike txt-link" data-like="" data-id="15078" title=""><span class="cmt_count"></span> thích</a>
                    <span class="space"> • </span>
                    <time class="reivews__date" datetime="2022-07-05T10:25:24+07:00">05/07/2022</time>
                </div>
            </div>
        </li>
        <li class="reviews__list-item">
            <div class="reviews__list-inner">
                <div class="comment-text">
                    <h3 class="reviews__name">Quan Nguyen</h3>
                    <div class="reviews__description">
                        <div class="rating">
                            <div class="rating__inner">
                                <span class="rating__pre" style="width: 65%"></span>
                            </div>
                        </div>
                        <p>Loa nghe khá ngon trong tầm giá mua lúc khuyến mãi nghe khá hay mình khó tính vẫn thấy ưng</p>
                    </div>
                </div>
                <div class="reviews__action d-flex align-items-center">
                    <a rel="nofollow" class="comment-reply-link txt-link" href="#comment-15078" data-commentid="15078" data-postid="69376" data-belowelement="comment-15078" data-respondelement="respond" data-replyto="Phản hồi đến Hai Nguyen" aria-label="Phản hồi đến Hai Nguyen">Trả lời</a>
                    <span class="space"> • </span>
                    <a href="javascript:void(0)" class="cmtlike txt-link" data-like="" data-id="15078" title=""><span class="cmt_count"></span> thích</a>
                    <span class="space"> • </span>
                    <time class="reivews__date" datetime="2022-07-05T10:25:24+07:00">05/07/2022</time>
                </div>
            </div>
        </li>
    </ol>
</div>
<div class="reviews__entry-form">
    <h6>Hỏi Đáp</h6>
    <form action="#" class="reviews__form">
        <div class="form-group">
            <textarea name="" id="" rows="5" placeholder="Mời bạn tham gia thảo luận, vui lòng nhập tiếng Việt có dấu." class="form-control"></textarea>
        </div>
        <div class="row align-items-center justify-content-end">
            <div class="col-12 col-sm-4 form-check d-flex align-items-center justify-content-sm-end mb-3 mb-sm-0">
                <div class="radio ps-3 me-5">
                    <label class="form-check-label">
                        <input type="radio" name="sex" class="form-check-input" checked />
                        <span>Anh</span>
                    </label>
                </div>
                <div class="radio">
                    <label class="form-check-label">
                        <input type="radio" name="sex" class="form-check-input" />
                        <span>Chị</span>
                    </label>
                </div>
            </div>
            <div class="col-6 col-sm-3 mb-3 mb-sm-0 pe-2">
                <input type="text" name="name" placeholder="Họ & Tên" class="form-control" />
            </div>
            <div class="col-6 col-sm-3 px-2 mb-3 mb-sm-0">
                <input type="text" name="telephone" placeholder="Số điện thoại" class="form-control" />
            </div>
            <div class="col-12 col-sm-2 ps-2 col-md-1">
                <button type="submit" class="btn btn-primary d-block">Gửi</button>
            </div>
        </div>
    </form>
</div>