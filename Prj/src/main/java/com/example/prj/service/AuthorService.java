package com.example.prj.service;

// Импорт сущности Author
import com.example.prj.entity.Author;
// Импорт репозитория AuthorRepository
import com.example.prj.repository.AuthorRepository;
// Аннотация @Service указывает, что это сервисный компонент Spring
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
// Сервисный класс для управления бизнес-логикой, связанной с Author
public class AuthorService {

    // Поле для взаимодействия с AuthorRepository
    private final AuthorRepository authorRepository;

    // Конструктор для внедрения зависимости AuthorRepository
    public AuthorService(AuthorRepository authorRepository) {
        this.authorRepository = authorRepository;
    }

    // Метод для получения всех авторов из базы данных
    public List<Author> getAllAuthors() {
        return authorRepository.findAll();
    }

    // Метод для получения автора по ID
    public Optional<Author> getAuthorById(Long id) {
        return authorRepository.findById(id);
    }

    // Метод для сохранения нового автора в базу данных
    public Author saveAuthor(Author author) {
        return authorRepository.save(author);
    }

    // Метод для обновления данных автора
    public Author updateAuthor(Long id, Author updatedAuthor) {
        // Поиск автора по ID и обновление его данных, если он существует
        return authorRepository.findById(id)
                .map(existingAuthor -> {
                    existingAuthor.setFirstName(updatedAuthor.getFirstName());
                    existingAuthor.setLastName(updatedAuthor.getLastName());
                    return authorRepository.save(existingAuthor);
                })
                // Если автор не найден, выбрасывается исключение
                .orElseThrow(() -> new RuntimeException("Author not found"));
    }

    // Метод для удаления автора по ID
    public void deleteById(Long id) {
        authorRepository.deleteById(id);
    }
}
