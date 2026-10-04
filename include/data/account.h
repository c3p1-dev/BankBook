#ifndef ACCOUNT_H
#define ACCOUNT_H

#include <string>
#include <cstdint>
#include <optional>
#include <chrono>

class account
{
public:
    std::uint64_t id() const;
    void id(std::uint64_t account_id);

    std::int64_t initial_balance() const;
    void initial_balance(std::int64_t cents);

    const std::string& code() const;
    void code(std::string account_code);

    const std::string& name() const;
    void name(std::string account_name);

    const std::optional<std::string>& bank() const;
    void bank(std::optional<std::string> bank_name);

    const std::optional<std::string>& description() const;
    void description(std::optional<std::string> account_description);

    const std::optional<std::string>& iban() const;
    void iban(std::optional<std::string> account_iban);

    const std::optional<std::string>& swift() const;
    void swift(std::optional<std::string> account_swift);

    const std::optional<std::string>& url() const;
    void url(std::optional<std::string> account_url);

    std::optional<std::chrono::system_clock::time_point> locked_at() const;
    void locked_at(std::optional<std::chrono::system_clock::time_point> lock_time);

private:
    std::uint64_t id_{0};
    std::int64_t initial_balance_{0};

    std::string code_;
    std::string name_;

    std::optional<std::string> bank_;
    std::optional<std::string> description_;
    std::optional<std::string> iban_;
    std::optional<std::string> swift_;
    std::optional<std::string> url_;
    std::optional<std::chrono::system_clock::time_point> locked_at_;
};

#endif // ACCOUNT_H