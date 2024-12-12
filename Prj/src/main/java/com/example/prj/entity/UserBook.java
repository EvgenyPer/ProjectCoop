//package com.example.prj.entity;
//
//import jakarta.persistence.*;
//
//@Entity
//public class UserBook {
//    @Id
//    @GeneratedValue(strategy = GenerationType.IDENTITY)
//    private Integer confirmationCode;
//
//    @ManyToOne
//    @JoinColumn(name = "user_id", nullable = false)
//    private User user;
//
//    @ManyToOne
//    @JoinColumn(name = "book_id", nullable = false)
//    private Book book;
//}