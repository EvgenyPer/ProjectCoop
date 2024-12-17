package com.example.prj.repository;

import com.example.prj.entity.Book;
import org.springframework.data.jpa.repository.JpaRepository;

// Интерфейс JpaRepository предоставляет базовые методы для работы с таблицей книг (CRUD-операции).
// Он автоматически предоставляет реализацию методов для взаимодействия с базой данных.
public interface BookRepository extends JpaRepository<Book, Long> {
}
