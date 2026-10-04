CXX       = c++
CXXFLAGS  = -O2 -Wall -std=c++20 -Iinclude
DEPFLAGS  = -MMD -MP
LDLIBS    = -lsqlite3

OUT       = bankbook
SRCDIR    = src
OBJDIR    = build

SRCS     := $(shell find $(SRCDIR) -type f -name '*.cpp')
OBJS     := $(patsubst $(SRCDIR)/%.cpp,$(OBJDIR)/%.o,$(SRCS))

all: $(OUT)

$(OUT): $(OBJS)
	$(CXX) $(LDFLAGS) -o $@ $(OBJS) $(LDLIBS)

$(OBJDIR)/%.o: $(SRCDIR)/%.cpp
	@mkdir -p $(@D)
	$(CXX) $(CXXFLAGS) $(DEPFLAGS) -c $< -o $@

-include $(OBJS:.o=.d)

clean:
	rm -rf $(OBJDIR) $(OUT)

run: $(OUT)
	./$(OUT)

.PHONY: all clean run
